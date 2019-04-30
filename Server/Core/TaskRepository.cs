using System.Collections.Generic;

namespace Core
{
    public class TaskRepository : ITaskRepository
    {
        public static List<Task> _store = new List<Task>();

        public void Add(Task task)
        {
            _store.Add(task);
        }

        public Task FindById(int id)
        {
            return _store.Find(it => it.Id == id);
        }
    }
}
