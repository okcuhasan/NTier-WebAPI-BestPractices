using ManagementSystem.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Repositories.Abstracts
{
    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetActives();
        IQueryable<T> GetModifieds();
        IQueryable<T> GetPassives();

        void Add(T entity);
        Task AddAsync(T entity);
        void AddRange(List<T> entities);
        Task AddRangeAsync(List<T> entities);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void UpdateRange(List<T> entities);
        Task UpdateRangeAsync(List<T> entities);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        void DeleteRange(List<T> entities);
        Task DeleteRangeAsync(List<T> entities);
        void Destroy(T entity);
        Task DestroyAsync(T entity);
        void DestroyRange(List<T> entities);
        Task DestroyRangeAsync(List<T> entities);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        IQueryable<X> Select<X>(Expression<Func<T, X>> expression);

        T Find(int id);
        Task<T> FindAsync(int id);
    }
}
