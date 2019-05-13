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

        string GetErrorCode(Exception exception);
    }


    public abstract class AbsErrorCodeGenerator<T> : IErrorCodeGenerator
        where T : Exception
    {

        public string ExceptionTypeFullName
        {
            get
            {
                return typeof(T).FullName;
            }
        }

        public string GetErrorCode(Exception exception)
        {
            if(null == exception)
            {
                throw new ErrorCodeGeneratorException("Input exception can not be null.");
            }

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
