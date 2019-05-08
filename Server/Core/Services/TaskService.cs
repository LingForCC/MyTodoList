using System;
using System.Collections.Generic;
using Core.Exceptions;
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
                throw new TaskException("task not found.")
                {
                    StatusCode = 404,
                };
            }

            task.Complete();

            _taskRepository.Update(task);

            _unitOfWork.Complete();
        }

        public Task CreateTask(string name)
        {
            try
            {
                Task task = new Task(name);
                this._taskRepository.Add(task);
                this._unitOfWork.Complete();
                return task;
            }
            catch (Exception e)
            {
                throw new ServiceException(nameof(TaskService), "Error Happens when Creating Task", e);
            }
        }

        public void DeleteTask(string taskId)
        {
            try
            {
                var task = _taskRepository.FindById(taskId);

                if (task == null)
                {
                    throw new TaskException("you're trying to delete a non-existing task.")
                    {
                        StatusCode = 404,
                    };
                }

                this._taskRepository.Delete(task);
                this._unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new ServiceException(nameof(TaskService), "Error Happens when Deleting Task", e);
            }
        }

        public Task GetTask(string taskId)
        {
            var task = _taskRepository.FindById(taskId);

            if (task == null)
            {
                throw new TaskException("task not found.")
                {
                    StatusCode = 404,
                };
            }

            return task;
        }
    }
}