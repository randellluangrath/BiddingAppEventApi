using System.Collections.Generic;
using WebAppEventApi.Models.Event;

namespace WebAppEventApi.Utilities
{
    public interface ITransformHelper
    {
        List<EventEntity> Transform(List<Event> events);
    }
}