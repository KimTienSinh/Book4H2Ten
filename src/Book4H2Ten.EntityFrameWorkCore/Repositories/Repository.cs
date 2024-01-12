
using Book4H2Ten.Core.Errors;
using Book4H2Ten.Entities;
using Book4H2Ten.EntityFrameWorkCore.EFExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.EntityFrameWorkCore.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        IQueryable<T> GetQuery();
        IQueryable<T> GetQuery(Expression<Func<T, bool>> filterCondition);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entities);
        Task HardDeleteAsync(T entity);
    }
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IUnitOfWork<Book4H2TenDbContext> _unitOfWork;

        public Repository(IUnitOfWork<Book4H2TenDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                entity.IsDeleted = false;
                await _unitOfWork.Context.Set<T>().AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
            }
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            try
            {
                await _unitOfWork.Context.Set<T>().AddRangeAsync(entities);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _unitOfWork.Context.Set<T>().Remove(entity);
                _unitOfWork.Context.ChangeTracker.SetAuditProperties();
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
            }
        }

        public async Task DeleteRangeAsync(List<T> entities)
        {
            try
            {
                _unitOfWork.Context.Set<T>().RemoveRange(entities);
                _unitOfWork.Context.ChangeTracker.SetAuditProperties();
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
            }
        }

        public async Task HardDeleteAsync(T entity)
        {
            try
            {
                _unitOfWork.Context.Set<T>().Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
            }
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.GuidId == id && !x.IsDeleted) ?? throw new NotFoundException("Not Found!");
        }

        public IQueryable<T> GetQuery()
        {
            return _unitOfWork.Context.Set<T>().AsNoTracking().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> filterCondition)
        {
            return _unitOfWork.Context.Set<T>().AsNoTracking().Where(x => !x.IsDeleted).Where(filterCondition);
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                _unitOfWork.Context.Set<T>().Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
            }
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            try
            {
                entities = entities.Select(entity =>
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    return entity;
                }).ToList();

                _unitOfWork.Context.Set<T>().UpdateRange(entities);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
            }
        }
    }
}
