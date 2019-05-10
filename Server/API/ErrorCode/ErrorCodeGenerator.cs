using System;
using Core.Services;

namespace API.ErrorCode
{
    public interface IErrorCodeGenerator
    {
        string ExceptionTypeFullName
        {
            get;
        }

        string GetErrorCode(ServiceException exception);
    }


    public abstract class AbsErrorCodeGenerator<T> : IErrorCodeGenerator
        where T : ServiceException
    {

        AbsErrorCodeGenerator()
        {
            ExceptionTypeFullName = typeof(T).FullName;
        }

        public string ExceptionTypeFullName
        {
            get;
            private set;
        }

        public string GetErrorCode(ServiceException exception)
        {
            if (exception is T)
            {
                return GetErrorCodeInternal(exception as T);
            }

            throw new ErrorCodeGeneratorException("An exception of type " +
                exception.GetType().FullName + " is sent to ErrorCodeGenerator " +
                	"for " + ExceptionTypeFullName);
        }

        public abstract string GetErrorCodeInternal(T exception);
    }

}
