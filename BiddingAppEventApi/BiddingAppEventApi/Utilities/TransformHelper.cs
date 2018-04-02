using System.Collections.Generic;
using System.Linq;
using WebAppEventApi.Models.Event;
using System;

namespace WebAppEventApi.Utilities
{
    public class TransformHelper : ITransformHelper
    {
        public List<EventEntity> Transform(List<Event> events)
        {
            return events.Select(CreateFlattenedEvent).ToList();
        }

        private static EventEntity CreateFlattenedEvent(Event evt)
        {
            return new EventEntity
            {
                callID = evt.callID,
                originatingNumber = evt.callDetails.originatingNumber,
                receivingNumber = evt.callDetails.receivingNumber,
                startTime = evt.callDetails.startTime,
                callDuration = evt.callDetails.callDuration,
                callType = evt.callDetails.callType,
                isConnected = evt.callDetails.isConnected
            };
        }
    }
}