using ManagementSystem.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ManagerServices.Abstracts
{
    public interface IManager<T> where T : class, IEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetActives();
        IQueryable<T> GetModifieds();
        IQueryable<T> GetPassives();

        string Add(T entity);
        Task<string> AddAsync(T entity);
        string AddRange(List<T> entities);
        Task<string> AddRangeAsync(List<T> entities);
        string Update(T entity);
        Task<string> UpdateAsync(T entity);
        string UpdateRange(List<T> entities);
        Task<string> UpdateRangeAsync(List<T> entities);
        string Delete(T entity);
        Task<string> DeleteAsync(T entity);
        string DeleteRange(List<T> entities);
        Task<string> DeleteRangeAsync(List<T> entities);
        string Destroy(T entity);
        Task<string> DestroyAsync(T entity);
        string DestroyRange(List<T> entities);
        Task<string> DestroyRangeAsync(List<T> entities);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        IQueryable<X> Select<X>(Expression<Func<T, X>> expression);

        T Find(int id);
        Task<T> FindAsync(int id);
    }
}
