using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Gateway.IRepositories;

namespace ProjectManagementWebApp.Gateway.Repositories
{
    public class Repository<TEntity>:IRepository<TEntity> where TEntity:class
    {
        private DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> prediction)
        {
            return context.Set<TEntity>().Where(prediction).ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> prediction)
        {
            return context.Set<TEntity>().Where(prediction).FirstOrDefault();
        }

        public TEntity FindById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().UpdateRange(entities);
        }

        public void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public bool IsExists(Expression<Func<TEntity, bool>> prediction)
        {
            return context.Set<TEntity>().Any(prediction);
        }

        public void RemoveList(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
