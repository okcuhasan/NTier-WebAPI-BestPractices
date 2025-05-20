using ManagementSystem.BLL.ManagerServices.Abstracts;
using ManagementSystem.DAL.Repositories.Abstracts;
using ManagementSystem.ENTITIES.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ManagerServices.Concretes
{
    public class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        private readonly IRepository<T> _repository;
        public BaseManager(IRepository<T> repository)
        {
            _repository = repository;
        }

        public string Add(T entity)
        {
            if (entity == null)
            {
                throw new ApplicationException("Eklenecek kayıt geçersiz");
            }

            try
            {
                _repository.Add(entity);
                return "Ekleme işlemi başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ekleme sırasında hata oluştu: {ex.Message}");
            }
        }

        public async Task<string> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ApplicationException("Eklenecek kayıt geçersiz");
            }

            try
            {
                await _repository.AddAsync(entity);
                return "Ekleme işlemi başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ekleme sırasında hata oluştu: {ex.Message}");
            }
        }

        public string AddRange(List<T> entities)
        {
            if(entities == null || entities.Count == 0)
            {
                throw new ApplicationException("Eklenecek kayıtlar geçersiz");
            }

            try
            {
                _repository.AddRange(entities);
                return "Ekleme işlemleri başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ekleme işlemleri sırasında hata oluştu: {ex.Message}");
            }
        }

        public async Task<string> AddRangeAsync(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ApplicationException("Eklenecek kayıtlar geçersiz");
            }

            try
            {
                await _repository.AddRangeAsync(entities);
                return "Ekleme işlemleri başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ekleme işlemleri sırasında hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public string Delete(T entity)
        {
            if (entity == null || entity.Id == 0)
            {
                throw new ApplicationException("Silinecek kayıt geçersiz");
            }

            try
            {
                _repository.Delete(entity);
                return "Silme işlemi başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Silme işlemi sırasında hata oluştu: {ex.Message}");
            }
        }

        public async Task<string> DeleteAsync(T entity)
        {
            if (entity == null || entity.Id == 0)
            {
                throw new ApplicationException("Silinecek kayıt geçersiz");
            }

            try
            {
                await _repository.DeleteAsync(entity);
                return "Silme işlemi başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Silme işlemi sırasında hata oluştu: {ex.Message}");
            }
        }

        public string DeleteRange(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ApplicationException("Silinecek kayıtlar geçersiz");
            }

            try
            {
                _repository.DeleteRange(entities);
                return "Silme işlemleri başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Silme işlemleri sırasında hata oluştu: {ex.Message}");
            }
        }

        public async Task<string> DeleteRangeAsync(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ApplicationException("Silinecek kayıtlar geçersiz");
            }

            try
            {
                await _repository.DeleteRangeAsync(entities);
                return "Silme işlemleri başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Silme işlemleri sırasında hata oluştu: {ex.Message}");
            };
        }

        public string Destroy(T entity)
        {
            if (entity == null || entity.Id == 0)
            {
                throw new ApplicationException("Silinecek kayıt geçersiz");
            }

            try
            {
                _repository.Destroy(entity);
                return "Silme işlemi başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Silme işlemi sırasında hata oluştu: {ex.Message}");
            }
        }

        public async Task<string> DestroyAsync(T entity)
        {
            if (entity == null || entity.Id == 0)
            {
                throw new ApplicationException("Silinecek kayıt geçersiz");
            }

            try
            {
                await _repository.DestroyAsync(entity);
                return "Silme işlemi başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Silme işlemi sırasında hata oluştu: {ex.Message}");
            }
        }

        public string DestroyRange(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ApplicationException("Silinecek kayıtlar geçersiz");
            }

            try
            {
                _repository.DestroyRange(entities);
                return "Silme işlemleri başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Silme işlemleri sırasında hata oluştu: {ex.Message}");
            }
        }

        public async Task<string> DestroyRangeAsync(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ApplicationException("Silinecek kayıtlar geçersiz");
            }

            try
            {
                await _repository.DestroyRangeAsync(entities);
                return "Silme işlemleri başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Silme işlemleri sırasında hata oluştu: {ex.Message}");
            };
        }

        public T Find(int id)
        {
            if(id == 0)
            {
                throw new ApplicationException("ID değeri geçersiz");
            }

            return _repository.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            if (id == 0)
            {
                throw new ApplicationException("ID değeri geçersiz");
            }

            return await _repository.FindAsync(id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetActives()
        {
            return _repository.GetActives();
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<T> GetModifieds()
        {
            return _repository.GetModifieds();
        }

        public IQueryable<T> GetPassives()
        {
            return _repository.GetPassives();
        }

        public IQueryable<X> Select<X>(Expression<Func<T, X>> expression)
        {
            return _repository.Select(expression);
        }

        public string Update(T entity)
        {
            if (entity == null || entity.Id == 0)
            {
                throw new ApplicationException("Güncellenecek kayıt geçersiz");
            }

            try
            {
                var existingData = _repository.Find(entity.Id);
                if(existingData == null)
                {
                    throw new ApplicationException("Güncellenecek kayıt bulunamadı");
                }

                _repository.Update(entity);
                return "Güncelleme işlemi başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Güncelleme işlemi sırasında hata oluştu: {ex.Message}");
            };
        }

        public async Task<string> UpdateAsync(T entity)
        {
            if (entity == null || entity.Id == 0)
            {
                throw new ApplicationException("Güncellenecek kayıt geçersiz");
            }

            try
            {
                var existingData = _repository.Find(entity.Id);
                if (existingData == null)
                {
                    throw new ApplicationException("Güncellenecek kayıt bulunamadı");
                }

                await _repository.UpdateAsync(entity);
                return "Güncelleme işlemi başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Güncelleme işlemi sırasında hata oluştu: {ex.Message}");
            };
        }

        public string UpdateRange(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ApplicationException("Güncellenecek kayıtlar geçersiz");
            }

            try
            {
                _repository.UpdateRange(entities);
                return "Güncelleme işlemleri başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Güncelleme işlemleri sırasında hata oluştu: {ex.Message}");
            };
        }

        public async Task<string> UpdateRangeAsync(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ApplicationException("Güncellenecek kayıtlar geçersiz");
            }

            try
            {
                await _repository.UpdateRangeAsync(entities);
                return "Güncelleme işlemleri başarı ile gerçekleştirildi";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Güncelleme işlemleri sırasında hata oluştu: {ex.Message}");
            };
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
