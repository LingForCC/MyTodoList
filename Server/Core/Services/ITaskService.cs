using System.Collections.Generic;

namespace Core.Services
{
    public interface ITaskService
    {
        IEnumerable<Task> GetTasks();

        System.Threading.Tasks.Task<Task> CreateTask(string name);

        void CompleteTask(string taskId);

        void DeleteTask(string taskId);

        System.Threading.Tasks.Task<Task> GetTaskByIdAsync(string taskId);

        System.Threading.Tasks.Task<IEnumerable<Task>> GetTasksAsync();

    }
}