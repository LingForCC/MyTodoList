using Core.Repositories;

namespace Core
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        void Complete();
    }
}