using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Site.WebApi.Filters
{
    public class SuccessResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult)
            {
                context.Result = new ObjectResult(new {success = true, data = ((OkObjectResult) context.Result).Value});
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}