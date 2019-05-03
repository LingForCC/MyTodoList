using System.Collections.Generic;

namespace Core
{
    public interface ITaskRepository
    {
        void Add(Task task);

        Task FindById(string id);

        IEnumerable<Task> FindAll();
    }
}
