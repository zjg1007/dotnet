using Dnc.DataAccessRepository.Context;
using Dnc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dnc.DataAccessRepository.Repositories
{
    public class EntityRepository: IEntityRepository
    {
        readonly DbContext _entitiesContext;

        public EntityRepository(EntityDbContext context)
        {
            _entitiesContext = context;
        }

        public virtual void Save()
        {
            _entitiesContext.SaveChanges();
        }

        public virtual IQueryable<T> GetAll<T>() where T : class, IEntity, new() =>
            _entitiesContext.Set<T>();

        public virtual IQueryable<T> GetAll<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IEntity, new()
        {
            IQueryable<T> query = _entitiesContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }

        public virtual T GetSingle<T>(Guid id) where T : class, IEntity, new() =>
            GetAll<T>().SingleOrDefault(x => x.ID ==id.ToString());

        public virtual T GetSingle<T>(Guid id, params Expression<Func<T, object>>[] includeProperties) where T : class, IEntity, new()
        {
            IQueryable<T> query = _entitiesContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.SingleOrDefault(x=>x.ID==id.ToString());

        }

        public virtual T GetSingleBy<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new() =>
            _entitiesContext.Set<T>().SingleOrDefault(predicate);

        public virtual T GetSingleBy<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, IEntity, new()
        {
            IQueryable<T> query = _entitiesContext.Set<T>().Where(predicate);
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.SingleOrDefault(predicate);

        }

        public virtual IQueryable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new() =>
            _entitiesContext.Set<T>().Where(predicate);

        public virtual void Add<T>(T entity) where T : class, IEntity, new()
        {
            EntityEntry dbEntityEntry = _entitiesContext.Entry(entity);
            _entitiesContext.Set<T>().Add(entity);
        }

        public virtual void AddAndSave<T>(T entity) where T : class, IEntity, new()
        {
            Add(entity);
            Save();
        }

        public virtual void Edit<T>(T entity) where T : class, IEntity, new()
        {
            EntityEntry dbEntityEntry = _entitiesContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void EditAndSave<T>(T entity) where T : class, IEntity, new()
        {
            Edit(entity);
            Save();
        }

        public virtual void AddOrEdit<T>(T entity) where T : class, IEntity, new()
        {
            var p = GetAll<T>().FirstOrDefault(x => x.ID == entity.ID);
            if (p == null)
            {
                Add(entity);
            }
            else
            {
                Edit(entity);
            }
        }

        public virtual void AddOrEditAndSave<T>(T entity) where T : class, IEntity, new()
        {
            AddOrEdit(entity);
            Save();
        }

        public virtual void Delete<T>(T entity) where T : class, IEntity, new()
        {
            EntityEntry dbEntityEntry = _entitiesContext.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void DeleteAndSave<T>(T entity) where T : class, IEntity, new()
        {
            Delete(entity);
            Save();
        }
    }
}
