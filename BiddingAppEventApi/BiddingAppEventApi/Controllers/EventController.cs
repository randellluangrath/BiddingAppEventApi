using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAppEventApi.Logging;
using WebAppEventApi.Models.Event;
using WebAppEventApi.Processors;
using WebAppEventApi.Models.Response;

namespace WebAppEventApi.Controllers
{
    public class EventController : ApiController
    {
        private readonly IEventProcessor _processor;

        public EventController(IEventProcessor processor)
        {
            _processor = processor;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Insert(List<Event> events)
        {
            LogRequest(nameof(Insert), events);

            var numProcessed = await _processor.Insert(events);

            return Ok(new SuccessResponse { CountProcessed = numProcessed });
        }

        [HttpPut]
        public async Task<IHttpActionResult> AddOrUpdate(List<Event> events)
        {
            LogRequest(nameof(AddOrUpdate), events);

            var numProcessed = await _processor.AddOrUpdate(events);

            return Ok(new SuccessResponse{ CountProcessed = numProcessed });
        }

        private void LogRequest(string method, List<Event> events)
        {
            Logger.Info($"Received {method} for collection of {nameof(Event)}. Count: {events.Count}");

            events.ForEach(x =>
                Logger.Info(
                    $"Received callID: {x.callID}, originatingNumber: {x.callDetails.originatingNumber}, callDate: {x.callDetails.callDate}"));
        }
    }
}