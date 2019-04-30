using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

            if (task.Name.Trim() == string.Empty)
            {
                throw new TaskException("invalid task name.");
            }

            if (!Regex.IsMatch(task.Name, Ultils.Regessions.TASK_NAME_REG))
            {
                throw new TaskException("invalid task name.");
            }

            _store.Add(task);
        }

        public IEnumerable<Task> FindAll()
        {
            return _store;
        }

        public Task FindById(string id)
        {
            return _store.Find(it => it.Id == id);
        }
    }
}
