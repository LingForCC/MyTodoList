using System;
namespace Core.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        void AddTaskAsync(Task task);
    }
}
