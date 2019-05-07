using System;

namespace Core.Services
{
    public class ServiceException : Exception
    {

        public ServiceException(string serviceName, string message)
            : this(serviceName, message, null)
        {

        }

        public ServiceException(string serviceName, string message, Exception innerException)
            : base(message, innerException)
        {
            ServiceName = serviceName;
        }

        public string ServiceName
        {
            get;
            private set;
        }
    }
}
