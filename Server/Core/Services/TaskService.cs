using System;
using System.Collections.Generic;
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

        public IEnumerable<Task> GetTasks()
        {
            return _taskRepository.FindAll();
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

            task.ValidateTaskName();

            this._taskRepository.Add(task);
            this._unitOfWork.Complete();
        } 

        public void DeleteTask(string taskId)
        {
            var task = _taskRepository.FindById(taskId);

            if (task == null)
            {
                throw new TaskException("you're trying to delete an non-existing task.");
            }

            this._taskRepository.Delete(task);
            this._unitOfWork.Complete();
        }

       
    }
}