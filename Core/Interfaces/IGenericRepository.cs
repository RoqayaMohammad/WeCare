using Core.Models;
using Core.Specifictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T>
    {

        Task<T> GetByIdAsync(int id, string includeProperties = null);
        Task<IReadOnlyList<T>> ListAllAsync(string includeProperties=null);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetEntitiesWithSpec(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity, string includeProperties=null);
        Task DeleteAsync(T entity);
    }
}
