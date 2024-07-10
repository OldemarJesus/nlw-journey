using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is JourneyException)
            {
                var journeyException = (JourneyException) context.Exception;

                context.HttpContext.Response.StatusCode = (int) journeyException.GetStatusCode();

                var responseErrorJson = new ResponseErrorsJson(journeyException.GetErrorMessages());
                context.Result = new ObjectResult(responseErrorJson);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseErrorJson = new ResponseErrorsJson(new List<string> { ResourceErrorMessages.SERVER_ERROR_MESSAGE });
                context.Result = new ObjectResult(responseErrorJson);
            }
        }
    }
}
