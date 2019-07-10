using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Gateway.IRepositories
{
    public interface IRepository<TEntity> where TEntity:class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);
        IEnumerable<TEntity> Get(Expression<Func<TEntity,bool>> predication);
        IEnumerable<TEntity> GetAll();
        TEntity Find(Expression<Func<TEntity, bool>> predication);
        TEntity FindById(int id);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        bool IsExists(Expression<Func<TEntity, bool>> prediction);
        void RemoveList(IEnumerable<TEntity> entities);
    }
}
