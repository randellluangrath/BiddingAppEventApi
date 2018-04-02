using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Filters;
using WebAppEventApi.Logging;
using WebAppEventApi.Models.Event;
using WebAppEventApi.Models.Response;
using Newtonsoft.Json;
using WebAppEventApi.Exceptions;

namespace WebAppEventApi.Attributes
{
    public class HttpExceptionAttribute : ExceptionFilterAttribute
    {
        private const string DefaultContent = "An internal server error occurred";
        private const string DefaultReasonPhrase = "Internal Server Error";

        public override void OnException(HttpActionExecutedContext context)
        {
            Logger.Error(context.Exception);

            var content = context.Exception is DbSaveException
                ? GetDbExceptionContent((DbSaveException)context.Exception) 
                : new StringContent(DefaultContent);

            ThrowException(content);
        }

        private static void ThrowException(HttpContent content)
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = content,
                ReasonPhrase = DefaultReasonPhrase
            });
        }

        private static StringContent GetDbExceptionContent(DbSaveException ex)
        {
            var failedId = ex.Message;

            var resp = new FailureResponse
            {
                FailingCallId = failedId
            };

            return new StringContent(JsonConvert.SerializeObject(resp), Encoding.UTF8, "application/json");
        }
    }
}