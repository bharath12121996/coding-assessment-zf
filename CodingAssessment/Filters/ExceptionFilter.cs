using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CodingAssessment.Domain.Model;

namespace CodingAssessment.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new ErrorModel
            (
                500,
                context.Exception.Message,
                context.Exception.StackTrace?.ToString()
            );

            context.Result = new JsonResult(error);
        }
    }
}
