using System;
using System.Collections.Generic;
using WebAppEventApi.Models.Event;

namespace WebAppEventApiTest.Helpers
{
    public static class EventHelper
    {
        public static List<Event> GetEventList()
        {
            var obj1 = new Event
            {
                callID = "callID",
                callDetails = new CallDetails
                {
                    originatingNumber = "1234567890",
                    receivingNumber = "1234567890",
                    startTime = DateTime.Now,
                    callDuration = DateTime.Now,
                    callType = "Voice",
                    isConnected = "Y"
                }
            };

            var obj2 = obj1;

            return new List<Event>{ obj1, obj2 };
        }

        public static List<EventEntity> GetEventEntityList()
        {
            var obj1 = new EventEntity
            {
                callID = "callID",
                originatingNumber = "1234567890",
                receivingNumber = "1234567890",
                startTime = DateTime.Now,
                callDuration= DateTime.Now,
                callType = "Voice",
                isConnected = "Y"
            };

            var obj2 = obj1;

            return new List<EventEntity> { obj1, obj2 };
        }
    }
}