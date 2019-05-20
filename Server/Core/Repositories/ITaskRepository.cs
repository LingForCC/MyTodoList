using System;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        Task<int> AddTaskAsync(Task task);
    }
}
