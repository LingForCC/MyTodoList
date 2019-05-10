using System;
using Core.Services;

namespace API.ErrorCode
{
    public interface IErrorCodeGenerator
    {
        string ExceptionFullName
        {
            get;
        }

        string GetErrorCode(ServiceException exception);
    }


    public abstract class AbsErrorCodeGenerator<T> : IErrorCodeGenerator
        where T : ServiceException
    {

        public string ExceptionFullName
        {
            get
            {
                return typeof(T).FullName;
            }
        }

        public string GetErrorCode(ServiceException exception)
        {
            if (exception is T)
            {
                return GetErrorCodeInternal(exception as T);
            }

            throw new Exception();
        }

        public abstract string GetErrorCodeInternal(T exception);

    }

}
