using CustomerOrder.API.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomerOrder.API.Application.Filters;

public class NotFoundExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) {}

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = new NotFoundResult();
            context.ExceptionHandled = true;
        }
    }
}
