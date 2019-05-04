namespace Core.Services
{
    public interface ITaskService
    {
        void CreateTask(Task task);

        void CompleteTask(string taskId);

        void DeleteTask(string taskId);
    }
}