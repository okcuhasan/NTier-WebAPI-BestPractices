using ManagementSystem.DAL.Context;
using ManagementSystem.DAL.Repositories.Abstracts;
using ManagementSystem.ENTITIES.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Repositories.Concretes
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public void Delete(T entity)
        {
            entity.Status = ENTITIES.Enums.DataStatus.Deleted;
            entity.DeletedDate = DateTime.Now;
            _context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            entity.Status = ENTITIES.Enums.DataStatus.Deleted;
            entity.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public void DeleteRange(List<T> entities)
        {
            foreach (T item in entities)
            {
                Delete(item);
            }
        }

        public async Task DeleteRangeAsync(List<T> entities)
        {
            foreach (T item in entities)
            {
                await DeleteAsync(item);
            }
        }

        public void Destroy(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task DestroyAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void DestroyRange(List<T> entities)
        {
            foreach (T item in entities)
            {
                Destroy(item);
            }
        }

        public async Task DestroyRangeAsync(List<T> entities)
        {
            foreach (T item in entities)
            {
                await DestroyAsync(item);
            }
        }

        public T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetActives()
        {
            return _context.Set<T>().Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetModifieds()
        {
            return _context.Set<T>().Where(x => x.Status == ENTITIES.Enums.DataStatus.Updated);
        }

        public IQueryable<T> GetPassives()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Deleted);
        }

        public IQueryable<X> Select<X>(Expression<Func<T, X>> expression)
        {
            return _context.Set<T>().Select(expression);
        }

        public void Update(T entity)
        {
            entity.Status = ENTITIES.Enums.DataStatus.Updated;
            entity.ModifiedDate = DateTime.Now;
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.Status = ENTITIES.Enums.DataStatus.Updated;
            entity.ModifiedDate = DateTime.Now;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public void UpdateRange(List<T> entities)
        {
            foreach (T item in entities)
            {
                Update(item);
            }
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            foreach (T item in entities)
            {
                await UpdateAsync(item);
            }
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
    }
}
