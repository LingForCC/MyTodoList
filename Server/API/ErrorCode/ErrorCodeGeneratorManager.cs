using System;
using System.Collections.Generic;
using Core.Services;

namespace API.ErrorCode
{
    public class ErrorCodeGeneratorManager
    {

        private readonly Dictionary<string, IErrorCodeGenerator> _errorCodeGenerators;

        public ErrorCodeGeneratorManager() 
        {
            _errorCodeGenerators = new Dictionary<string, IErrorCodeGenerator>();
        }

        public void RegisterErrorCodeGenerator(IErrorCodeGenerator errorCodeGenerator)
        {
            if(null == errorCodeGenerator)
            {
                throw new ErrorCodeGeneratorException("Input errorCodeGenerator can not be null!");
            }

            if (_errorCodeGenerators.ContainsKey(errorCodeGenerator.ExceptionTypeFullName))
            {
                throw new ErrorCodeGeneratorException("The ErrorCodeGenerator for " +
                    errorCodeGenerator.ExceptionTypeFullName + " has been added already.");
            }

            _errorCodeGenerators.Add(errorCodeGenerator.ExceptionTypeFullName, errorCodeGenerator);
        }

        public string GetErrorCode(ServiceException exception)
        {
            if(null == exception)
            {

                throw new ErrorCodeGeneratorException("Input exception can not be null!");
            }

            try
            {
                string exceptionTypeFullName = exception.GetType().FullName;

                if (_errorCodeGenerators.ContainsKey(exceptionTypeFullName))
                {
                    return _errorCodeGenerators[exceptionTypeFullName].GetErrorCode(exception);
                }

                //TODO: Log a warning that a ServiceException doesn't have dedicated ErrorCodeGenerator

                return "GEC-100";
            }
            catch(Exception e)
            {
                //TODO: Log a warning message that an exception is not properly handled by ErrorCodeGenerator

                return "GEC-100";
            }

        }
    }

}
