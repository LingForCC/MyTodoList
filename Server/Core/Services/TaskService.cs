using System;
using System.Text.RegularExpressions;
using Core.Repositories;

namespace Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Task> _taskRepository;

        public TaskService(IUnitOfWork unitOfWork, IRepository<Task> taskRepository)
        {
            this._unitOfWork = unitOfWork;
            this._taskRepository = taskRepository;
        }

        public void CompleteTask(string id)
        {
            var task = _taskRepository.FindById(id);
            if (task == null)
            {
                throw new TaskException("task not found.");
            }

            task.Complete();

            _taskRepository.Update(task);

            _unitOfWork.Complete();
        }

        public void CreateTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            this.ValidateTaskName(task.Name);

            this._taskRepository.Add(task);
            this._unitOfWork.Complete();
        }

        public void ValidateTaskName(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
            {
                throw new TaskException("invalid task name.");
            }

            if (!Regex.IsMatch(taskName, Ultils.Regessions.TASK_NAME_REG))
            {
                throw new TaskException("invalid task name.");
            }

            if (taskName.Trim().Length > Task.MAX_LENGTH_OF_NAME)
            {
                throw new TaskException("invalid task name.");
            }
        }

        public void DeleteTask(string taskId)
        {
            this._taskRepository.Delete(taskId);
            this._unitOfWork.Complete();
        }
    }
}