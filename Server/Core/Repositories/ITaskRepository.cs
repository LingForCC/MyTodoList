using System;

namespace Core.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        System.Threading.Tasks.Task<Task> AddTaskAsync(Task task);

        System.Threading.Tasks.Task<Task> FindByIdAsync(string id);
    }
}
