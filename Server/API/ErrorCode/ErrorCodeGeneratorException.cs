using System;
namespace API.ErrorCode
{
    public class ErrorCodeGeneratorException : Exception
    {
        public ErrorCodeGeneratorException() { }

        public ErrorCodeGeneratorException(string message) : base(message) { }

        public ErrorCodeGeneratorException(string message, Exception inner) : base(message, inner) { }
    }
}
