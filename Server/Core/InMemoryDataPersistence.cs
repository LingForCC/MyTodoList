using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Core
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {

        public void Complete()
        {
            // nothing to do.
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new InMemoryRepository<T>();
        }
    }

    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private static readonly List<T> _store = new List<T>();

        public string Name => throw new NotImplementedException();

        public void Add(T entity)
        {
            _store.Add(entity);
        }

        public void Delete(T entity)
        {
            _store.Remove(entity);
        }

        public void Delete(string id)
        {
            var entity = this.FindById(id);
            if (entity != null)
            {
                _store.Remove(entity);
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new NullReferenceException(nameof(predicate));
            }

            return _store.AsQueryable().Where(predicate).AsEnumerable();
        }

        public T FindById(string id)
        {
            return _store.Find(it => ((dynamic)it).Id == id);
        }

        public IEnumerable<T> FindAll()
        {
            return _store;
        }

        public void Update(T entity)
        {
            // nothing to do.
        }
    }
}