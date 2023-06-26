using Core.Interfaces;
using Core.Models;
using Core.Specifictions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly storeContext _context;
        public GenericRepository(storeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity; 
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id, string includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.FirstOrDefaultAsync(e => e.Id == id);
            // return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity, string includeProperties = null)
        {
            var existingEntity = await GetByIdAsync(entity.Id, includeProperties);

            if (existingEntity == null)
            {
                throw new ArgumentException($"Entity with Id {entity.Id} not found");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            // Set only the modified fields to the Modified state
            foreach (var property in _context.Entry(existingEntity).Properties)
            {
                if (property.IsModified && property.CurrentValue != null)
                {
                    _context.Entry(existingEntity).Property(property.Metadata).IsModified = true;
                }
            }

            // Include related entities in the query
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var includePropertiesArray = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var prop in includePropertiesArray)
                {
                    _context.Entry(existingEntity).Reference(prop).Load();
                }
            }

            await _context.SaveChangesAsync();
            // _context.Entry(entity).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<T>> ListAllAsync(string includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetEntitiesWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }


    }
}
