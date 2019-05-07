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
                var exception = context.Exception.InnerException as DomainException ?? context.Exception as DomainException;

                var msg = exception.Message;

                context.Result = new ObjectResult(new StandardErrorResponseModel
                {
                    Message = msg,
                    ErrorCode = exception.ErrorCode,
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
