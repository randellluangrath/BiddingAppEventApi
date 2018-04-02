using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WebAppEventApi.Controllers;
using WebAppEventApi.Models.Event;
using WebAppEventApi.Models.Response;
using WebAppEventApi.Processors;
using Moq;
using Xunit;

namespace WebAppEventApiTest.Controllers
{
    public class EventControllerTest : TestBase<EventController>
    {
        private Mock<IEventProcessor> ProcessorMock { get; }

        public EventControllerTest()
        {
            ProcessorMock = new Mock<IEventProcessor>();

            InstantiateClassUnderTest(ProcessorMock.Object);
        }

        [Fact]
        public async Task EventController_Insert_WithListOfEvent_CallsProcessor()
        {
            var evt = new Event
            {
                callID = "ID",
                callDetails = new CallDetails
                {
                    isConnected = "Y"
                }
            };

            var list = new List<Event> { evt };
            var expectedNum = 1;

            ProcessorMock.Setup(x => x.Insert(list)).Returns(Task.FromResult(expectedNum));

            var response = await ClassUnderTest.Insert(list);

            ProcessorMock.Verify(x => x.Insert(list), Times.Once());
            var content = Assert.IsType<OkNegotiatedContentResult<SuccessResponse>>(response).Content;
            Assert.Equal(content.CountProcessed, expectedNum);
        }

        [Fact]
        public async Task EventController_AddOrUpdate_WithListOfEvent_CallsProcessor()
        {
            var evt = new Event
            {
                callID = "ID",
                callDetails = new CallDetails
                {
                    isConnected = "Y"
                }
            };

            var list = new List<Event>{ evt };
            var expectedNum = 1;

            ProcessorMock.Setup(x => x.AddOrUpdate(list)).Returns(Task.FromResult(expectedNum));

            var response = await ClassUnderTest.AddOrUpdate(list);

            ProcessorMock.Verify(x => x.AddOrUpdate(list), Times.Once());
            var content = Assert.IsType<OkNegotiatedContentResult<SuccessResponse>>(response).Content;
            Assert.Equal(content.CountProcessed, expectedNum);
        }
    }
}
