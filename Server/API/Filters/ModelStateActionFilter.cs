using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace API.Filters
{
    public class ModelStateActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Select(it => new UnifyExceptionError
                {
                    Field = it.Key,
                    Description = it.Value.Errors.FirstOrDefault().ErrorMessage,
                });

                context.Result = new ObjectResult(errors)
                {
                    StatusCode = 400,
                    DeclaredType = typeof(IEnumerable<UnifyExceptionError>)
                };
            }
        }
    }

    public class UnifyExceptionError
    {
        public string Field { get; set; }

        public string Description { get; set; }
    }
}
