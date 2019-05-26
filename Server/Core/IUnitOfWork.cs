using Core.Repositories;

namespace Core
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        int Complete();

        System.Threading.Tasks.Task<int> CompleteAsync();
    }
}