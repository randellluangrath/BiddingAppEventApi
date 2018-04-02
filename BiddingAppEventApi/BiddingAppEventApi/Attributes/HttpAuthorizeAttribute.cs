using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebAppEventApi.Logging;
using WebAppEventApi.Utilities;

namespace WebAppEventApi.Attributes
{
    public class HttpAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly IConfigurationUtility _configUtility;
        private const string Scheme = "ApiKey";
        private const string ReasonPhrase = "Unauthorized";

        public HttpAuthorizeAttribute(IConfigurationUtility configUtility)
        {
            _configUtility = configUtility;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (IsAuthHeaderValid(actionContext.Request.Headers.Authorization))
            {
                IsAuthorized(actionContext);
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
            {
                ReasonPhrase = ReasonPhrase
            };

            Logger.Error($"Unauthorized request sent to {actionContext.Request.RequestUri.LocalPath}");
        }

        private bool IsAuthHeaderValid(AuthenticationHeaderValue authHeader)
        {
            return authHeader != null && authHeader.Scheme.Equals(Scheme) &&
                   authHeader.Parameter.Equals(_configUtility.ApiKey);
        }
    }
}