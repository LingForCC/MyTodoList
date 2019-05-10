using API.Models;
using Core.Exceptions;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class GlobalExpcetionHandler : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainException || context.Exception is ServiceException)
            {
                var exception = context.Exception.InnerException ?? context.Exception;

                var svcExp = context.Exception as ServiceException;
                string combinedErrorCode = svcExp?.ToString();

                context.Result = new ObjectResult(new StandardErrorResponseModel
                {
                    Message = exception?.Message,
                    ErrorCode = combinedErrorCode,
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            else
            {
                context.Result = new ObjectResult(new StandardErrorResponseModel
                {
                    Message = "unexpected error. please retry later."
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
