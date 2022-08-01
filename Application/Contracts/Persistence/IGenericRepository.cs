using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using Application.Models.Responses;

namespace Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<IReadOnlyList<T>> GetAll();
        Task<PaginationResponse<T>> GetAll(PaginatedFilter paginatedFilter);
        Task<T> Add(T entity);
        Task<bool> Exists(Guid id);
        Task Update(T entity);
        Task Delete(T entity);
        Task Save();
    }
}
