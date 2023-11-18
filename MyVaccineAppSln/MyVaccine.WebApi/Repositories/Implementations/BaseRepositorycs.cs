using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyVaccine.WebApi.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly MyVaccineAppDbContext _context;

        public BaseRepository(MyVaccineAppDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");
            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetValue(entity, DateTime.UtcNow);
            }

            var createdAtProperty = entity.GetType().GetProperty("CreatedAt");
            if (createdAtProperty != null)
            {
                createdAtProperty.SetValue(entity, DateTime.UtcNow);
            }

            await _context.AddAsync(entity);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();

        }

        public async Task AddRange(List<T> entity)
        {
            entity = entity.Select(x =>
            {
                var updatedAtProperty = x.GetType().GetProperty("UpdatedAt");
                if (updatedAtProperty != null)
                {
                    updatedAtProperty.SetValue(x, DateTime.UtcNow);
                }

                var createdAtProperty = x.GetType().GetProperty("CreatedAt");
                if (createdAtProperty != null)
                {
                    createdAtProperty.SetValue(x, DateTime.UtcNow);
                }

                return x;
            }).ToList();

            _context.AddRange(entity);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(T entity)
        {

            _context.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteRange(List<T> entity)
        {
            _context.RemoveRange(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<T> FindByAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return GetAll().AsNoTracking().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            var entitySet = _context.Set<T>();
            return entitySet.AsQueryable();
        }

        public async Task Patch(T entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                var key = entity.GetType().GetProperty("Id").GetValue(entity, null);

                var originalEntity = await _context.Set<T>().FindAsync(key);

                entry = _context.Entry(originalEntity);
                entry.CurrentValues.SetValues(entity);
            }

            var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");

            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetValue(entity, DateTime.UtcNow);
                entry.Property("UpdatedAt").IsModified = true;
            }
            var changedProperties = entry.Properties
            .Where(p => p.IsModified)
            .Select(p => p.Metadata.Name);

            foreach (var name in changedProperties)
            {
                entry.Property(name).IsModified = true;
            }

            await _context .SaveChangesAsync();

        }
        public  async Task PatchRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                var entry = _context.Entry(entity);

                if (entry.State == EntityState.Detached)
                {
                    var key = entity.GetType().GetProperty("Id").GetValue(entity, null);

                    var originalEntity = await _context.Set<T>().FindAsync(key);

                    entry = _context.Entry(originalEntity);
                    entry.CurrentValues.SetValues(entity);
                }

                var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");

                if (updatedAtProperty != null)
                {
                    updatedAtProperty.SetValue(entity, DateTime.UtcNow);
                    entry.Property("UpdatedAt").IsModified = true;
                }
                var changedProperties = entry.Properties
                .Where(p => p.IsModified)
                .Select(p => p.Metadata.Name);

                foreach (var name in changedProperties)
                {
                    entry.Property(name).IsModified = true;
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(T entity)
        {
            var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");
            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetValue(entity, DateTime.UtcNow);
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(List<T> entity)
        {
            entity = entity.Select(x =>
            {
                var updatedAtProperty = x.GetType().GetProperty("UpdatedAt");
                if (updatedAtProperty != null)
                {
                    updatedAtProperty.SetValue(x, DateTime.UtcNow);
                }

                return x;
            }).ToList();

            _context.UpdateRange(entity);
            await _context.SaveChangesAsync();
        }
    }
}
