using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppEventApi.Models.Event;

namespace WebAppEventApi.Processors
{
   public interface IEventProcessor
    {
        Task<int> Insert(List<Event> events);
        Task<int> AddOrUpdate(List<Event> events);
    }
}
