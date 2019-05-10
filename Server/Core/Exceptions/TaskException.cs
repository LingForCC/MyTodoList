using System;

namespace Core.Exceptions
{
    public class TaskException : DomainException
    {
        public TaskException(string message)
            : this(message, null)
        {

        }

        public TaskException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
