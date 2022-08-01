using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Models;
using Application.Models.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Extensions;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable
        where T : BaseEntity 
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> Get(Guid id)
        {
            return await _dbContext.Set<T>()
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>()
                .ToListAsync();
        }

        public async Task<PaginationResponse<T>> GetAll(PaginatedFilter paginatedFilter)
        {
            IQueryable<T> query = _dbContext.Set<T>()
                .ApplySearchFilter(paginatedFilter.SearchFilter);

            var totalCount = query.Count();

            var pageSize = paginatedFilter.PageSize > totalCount
                ? totalCount
                : paginatedFilter.PageSize;

            var totalPages = (totalCount == 0)
                ? 1
                : (int)Math.Ceiling((decimal)(totalCount / pageSize));

            var currentPage = paginatedFilter.PageNumber > totalPages
                ? totalPages
                : paginatedFilter.PageNumber;

            query = query
                .OrderBy(paginatedFilter.OrderBy, paginatedFilter.OrderIsDescending)
                .Skip(currentPage * pageSize)
                .Take(pageSize);

            var entities = await query.ToListAsync();

            var result = new PaginationResponse<T>
            {
                Data = entities,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                TotalCount = totalCount,
                PageSize = pageSize
            };

            return result;
        }

        public async Task<bool> Exists(Guid id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual Task Update(T entity)
        {
            _dbContext.Update(entity);
            return Task.CompletedTask;
        }

        public Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
