using System;
using System.Text.RegularExpressions;
using Core.Repositories;

namespace Core.Services
{
  public class TaskService : ITaskService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Task> _taskRepository;

    TaskService(IUnitOfWork unitOfWork) {
      this._unitOfWork = unitOfWork;
      this._taskRepository = unitOfWork.GetRepository<Task>();
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
      this.ValidateTaskName(task);
    }

    protected void ValidateTaskName(Task task)
    {
      if (null == task)
      {
          throw new ArgumentNullException(nameof(task));
      }

      if (string.IsNullOrWhiteSpace(task.Name))
      {
          throw new TaskException("invalid task name.");
      }

      if (!Regex.IsMatch(task.Name, Ultils.Regessions.TASK_NAME_REG))
      {
          throw new TaskException("invalid task name.");
      }
    }

    public Task GetTaskById(string id)
    {
      return this._taskRepository.FindById(id);
    }

    public void DeleteTask(string taskId)
    {
      this._taskRepository.Delete(taskId);
      this._unitOfWork.Complete();
    }
  }
}