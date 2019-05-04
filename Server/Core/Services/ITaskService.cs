namespace Core.Services
{
    public interface ITaskService
    {
        void CreateTask(Task task);

        Task GetTaskById(string taskId);

        void CompleteTask(string taskId);

        void DeleteTask(string taskId);
    }
}