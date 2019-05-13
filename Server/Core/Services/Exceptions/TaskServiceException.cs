using System;
namespace Core.Services.Exceptions
{
    public class TaskServiceException : ServiceException
    {

        public TaskServiceException(string message)
            : base(nameof(TaskService), message)
        {

        }

        public TaskServiceException(string message, Exception innerException)
            : base(nameof(TaskService), message, innerException)
        {

        }
    }

    public class TaskServiceCreationException : TaskServiceException
    {
        public TaskServiceCreationException(string message) 
            : base(message)
        {
        }

        public TaskServiceCreationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
