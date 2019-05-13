using System;
using Core.Services;

namespace API.ErrorCode
{
    public interface IErrorCodeGeneratorManager
    {
        void RegisterErrorCodeGenerator(IErrorCodeGenerator errorCodeGenerator);

        string GetErrorCode(Exception exception);
    }
}
