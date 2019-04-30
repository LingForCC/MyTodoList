using System;
using System.Collections.Generic;

namespace Core
{
    public class TaskRepository : ITaskRepository
    {
        private static List<Task> _store = new List<Task>();

        public void Add(Task task)
        {
            if (null == task)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _store.Add(task);
        }

        public Task FindById(string id)
        {
            return _store.Find(it => it.Id == id);
        }
    }
}
