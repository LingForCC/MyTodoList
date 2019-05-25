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

        public const string INVALID_TASK_NAME = "Task name is invalid!";
        public const string GENERIC_ERROR = "Error Happens when Creating Task";

        public TaskServiceCreationException(string message) 
            : base(message)
        {
        }

        public TaskServiceCreationException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }

    public class TaskServiceQueryException : TaskServiceException
    {

        public const string EMPTY_TASK_ID = "Task Id can not be null or empty!";
        public const string GENERIC_ERROR = "Error happens when querying tasks!";

        public TaskServiceQueryException(string message) 
            : base(message)
        {
        }

        public TaskServiceQueryException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }
}
