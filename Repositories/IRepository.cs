using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextFit.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
