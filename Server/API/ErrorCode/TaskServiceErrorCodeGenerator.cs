using System;
using Core.Services.Exceptions;

namespace API.ErrorCode
{
    public class TaskServiceCreationErrorCodeGenerator : AbsErrorCodeGenerator<TaskServiceCreationException>
    {

        public const string INVALID_TASK_NAME = "TSC-101";
        public const string GENERIC_ERROR = "TSC-100";

        public override string GetErrorCodeInternal(TaskServiceCreationException exception)
        {
            if(exception.Message == TaskServiceCreationException.INVALID_TASK_NAME)
            {
                return INVALID_TASK_NAME;
            }

            return GENERIC_ERROR;
        }
    }
}
