namespace Core.Services
{
    public interface ITaskService
    {
        void ValidateTaskName(string taskName);

        void CreateTask(Task task);

        void CompleteTask(string taskId);

        void DeleteTask(string taskId);
    }
}