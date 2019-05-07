using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class GlobalExpcetionHandler : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is TaskException)
            {
                var exception = context.Exception as TaskException;

                var response = exception.Message;

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 400,
                };
            }
            else
            {
                context.Result = new ObjectResult("unexpected error. please retry later.")
                {
                    StatusCode = 400,
                };
            }
        }
    }
}
