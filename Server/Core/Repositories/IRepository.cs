using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRepository<T> : IRepositoryAsync<T> where T : class
    {

        string Name { get; }

        T FindById(string id);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindAll();
          
        void Add(T entity);

        void Delete(T entity);

        void Delete(string id);

        void Update(T entity);
    }

    public interface IRepositoryAsync<T> where T: class {

        Task<T> FindByIdAsync(string id);

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> FindAllAsync();
    }
}