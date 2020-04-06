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

        public async System.Threading.Tasks.Task<Task> CreateTask(string name)
        {
            try
            {
                Task task = new Task(name);
                await  _taskRepository.AddAsync(task);
                var result = await this._unitOfWork.CompleteAsync();
                if(result != 1)
                {
                    throw new RepositoryException(_taskRepository.Name, 
                        "Unexpected error happens without throwing exception when adding task");
                }

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

        public async System.Threading.Tasks.Task<Task> GetTaskByIdAsync(string taskId)
        {
            if(string.IsNullOrEmpty(taskId)) {
                throw new TaskServiceQueryException(TaskServiceQueryException.EMPTY_TASK_ID, null);
            }
            
            try {
                return await _taskRepository.FindByIdAsync(taskId);
            }
            catch(Exception e) {
                throw new TaskServiceQueryException(TaskServiceQueryException.GENERIC_ERROR, e);
            }
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetTasksAsync()
        {
            try 
            {
                return await _taskRepository.FindAllAsync();
            }
            catch(Exception e) 
            {
                throw new TaskServiceQueryException(TaskServiceQueryException.GENERIC_ERROR, e);
            }
        }
    }


}