using System;
using System.Collections.Generic;
using Core.Exceptions;
using Core.Repositories;
using Core.Services.Exceptions;

namespace Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Task> _taskRepository;
        private readonly ITaskRepository _taskRepository2;

        public TaskService(IUnitOfWork unitOfWork, IRepository<Task> taskRepository,
            ITaskRepository taskRepository2)
        {
            this._unitOfWork = unitOfWork;
            this._taskRepository = taskRepository;
            _taskRepository2 = taskRepository2;
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

        public async System.Threading.Tasks.Task<Task> CreateTask(string name)
        {
            try
            {
                Task task = new Task(name);
                await  _taskRepository2.AddTaskAsync(task);

                //To be removed
                this._taskRepository.Add(task);
                this._unitOfWork.Complete();

                return task;
            }
            catch (InvalidNameTaskException e)
            {
                throw new TaskServiceCreationException(TaskServiceCreationException.INVALID_TASK_NAME, e);
            }
            catch(Exception e)
            {
                throw new TaskServiceCreationException(TaskServiceCreationException.GENERIC_ERROR, e);
            }
        }

        public void DeleteTask(string taskId)
        {
            try
            {
                var task = _taskRepository.FindById(taskId);

                if (task == null)
                {
                    throw new TaskException("you're trying to delete a non-existing task.");
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
                throw new TaskException("task not found.");
            }

            return task;
        }
    }


}