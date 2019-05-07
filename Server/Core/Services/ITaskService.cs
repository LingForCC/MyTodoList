using System.Collections.Generic;

namespace Core.Services
{
    public interface ITaskService
    {
        IEnumerable<Task> GetTasks();

        void CreateTask(Task task);

        void CompleteTask(string taskId);

        void DeleteTask(string taskId);
    }
}