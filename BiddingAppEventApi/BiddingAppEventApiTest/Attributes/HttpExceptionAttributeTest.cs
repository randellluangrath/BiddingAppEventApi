using System;
using System.Net;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Filters;
using WebAppEventApi.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Xunit.Assert;
using WebAppEventApi.Exceptions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using WebAppEventApi.Models.Response;

namespace WebAppEventApiTest.Attributes
{
    public class HttpExceptionAttributeTest : TestBase<HttpExceptionAttribute>
    {
        public HttpExceptionAttributeTest()
        {
            InstantiateClassUnderTest();
        }
            
        [Fact]
        public void HttpExceptionAttribute_WithException_ReturnsExceptionAndCorrectStatusCode()
        {
            var po = new PrivateObject(ClassUnderTest);
            var content = (string)po.GetField("DefaultContent", BindingFlags.NonPublic | BindingFlags.Static);
            var reasonPhrase = (string)po.GetField("DefaultReasonPhrase", BindingFlags.NonPublic | BindingFlags.Static);

            var exceptionParam = new HttpActionExecutedContext { Exception = new Exception() };

            var expectedException = Assert.Throws<HttpResponseException>(() => ClassUnderTest.OnException(exceptionParam));

            Assert.Equal(expectedException.Response.ReasonPhrase, reasonPhrase);
            Assert.Equal(expectedException.Response.Content.ReadAsStringAsync().Result, content);
            Assert.Equal(HttpStatusCode.InternalServerError, expectedException.Response.StatusCode);
        }

        [Fact]
        public void HttpExceptionAttribute_WithDbSaveException_ReturnsExceptionAndCorrectStatusCode()
        {
            var vehicleId = "12345";
            var po = new PrivateObject(ClassUnderTest);
            
            var reasonPhrase = (string)po.GetField("DefaultReasonPhrase", BindingFlags.NonPublic | BindingFlags.Static);

            var exceptionParam = new HttpActionExecutedContext { Exception = new DbSaveException(vehicleId)};

            var exception = Assert.Throws<HttpResponseException>(() => ClassUnderTest.OnException(exceptionParam));

            Assert.Equal(exception.Response.ReasonPhrase, reasonPhrase);
            Assert.Contains(vehicleId, exception.Response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.Response.StatusCode);
        }
    }
}
