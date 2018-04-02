using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppEventApi.DataAccess;
using WebAppEventApi.Models.Event;
using WebAppEventApi.Processors;
using WebAppEventApi.Utilities;
using Moq;
using Xunit;

namespace WebAppEventApiTest.Processors
{
    public class EventProcessorTest : TestBase<EventProcessor>
    {
        private Mock<IEventRepository> EventRepoMock { get; }

        private Mock<ITransformHelper> TransformHelperMock { get; }

        public EventProcessorTest()
        {
            EventRepoMock = new Mock<IEventRepository>();
            TransformHelperMock = new Mock<ITransformHelper>();

            InstantiateClassUnderTest(TransformHelperMock.Object, EventRepoMock.Object);
        }

        [Fact]
        public async Task EventProcessor_Insert_GetsDataTableAndPassesToRepository()
        {
            var list = new List<Event> { new Event() };
            var entitiesList = new List<EventEntity>();

            TransformHelperMock.Setup(x => x.Transform(list)).Returns(entitiesList).Verifiable();

            await ClassUnderTest.Insert(list);

            TransformHelperMock.Verify();
            EventRepoMock.Verify(x => x.InsertAsync(entitiesList), Times.Once());
        }

        [Fact]
        public async Task EventProcessor_AddOrUpdate_GetsDataTableAndPassesToRepository()
        {
            var list = new List<Event> { new Event() };
            var entitiesList = new List<EventEntity>();

            TransformHelperMock.Setup(x => x.Transform(list)).Returns(entitiesList).Verifiable();

            await ClassUnderTest.AddOrUpdate(list);

            TransformHelperMock.Verify();
            EventRepoMock.Verify(x => x.AddOrUpdateAsync(entitiesList), Times.Once());
        }
    }
}
