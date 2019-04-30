namespace Core
{
    public interface ITaskRepository
    {
        void Add(Task task);

        Task FindById(string id);
    }
}
