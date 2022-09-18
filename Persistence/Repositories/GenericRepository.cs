using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public virtual async Task<T> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>()
                .FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<PaginationResponse<T>> GetAllAsync(PaginatedFilter paginatedFilter, CancellationToken cancellationToken)
        {
            IQueryable<T> query = _dbContext.Set<T>().ApplySearchFilter(paginatedFilter.SearchFilter);

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

            var entities = await query.ToListAsync(cancellationToken);

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

        public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            return entity != null;
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            return entity;
        }

        public virtual void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
