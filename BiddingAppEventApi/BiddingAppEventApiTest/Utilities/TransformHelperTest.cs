using System.Collections.Generic;
using WebAppEventApi.Models.Event;
using WebAppEventApi.Utilities;
using WebAppEventApiTest.Helpers;
using Xunit;

namespace WebAppEventApiTest.Utilities
{
    public class TransformHelperTest : TestBase<TransformHelper>
    {
        public TransformHelperTest()
        {
            InstantiateClassUnderTest();
        }

        [Fact]
        public void TransformHelper_Transform_ReturnsFlattenedObject()
        {
            var list = EventHelper.GetEventList();

            var flattenedList = ClassUnderTest.Transform(list);

            AssertTransform(list, flattenedList);
        }

        private static void AssertTransform(IReadOnlyList<Event> list, IReadOnlyList<EventEntity> flattenedList)
        {
            for (var i = 0; i < list.Count; i++)
            {
                Assert.Equal(list[i].callID, flattenedList[i].callID);
                Assert.Equal(list[i].callDetails.originatingNumber, flattenedList[i].originatingNumber);
                Assert.Equal(list[i].callDetails.receivingNumber, flattenedList[i].receivingNumber);
                Assert.Equal(list[i].callDetails.startTime, flattenedList[i].startTime);
                Assert.Equal(list[i].callDetails.callDuration, flattenedList[i].callDuration);
                Assert.Equal(list[i].callDetails.callType, flattenedList[i].callType);
                Assert.Equal(list[i].callDetails.isConnected, flattenedList[i].isConnected);
            }
        }
    }
}
