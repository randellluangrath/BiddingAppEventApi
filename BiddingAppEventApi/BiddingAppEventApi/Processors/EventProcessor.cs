using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppEventApi.DataAccess;
using WebAppEventApi.Logging;
using WebAppEventApi.Models.Event;
using WebAppEventApi.Utilities;

namespace WebAppEventApi.Processors
{
    // This class is responsible for creating a data table and passing it to the
    // repository to save
    public class EventProcessor : IEventProcessor
    {
        private readonly ITransformHelper _transformHelper;
        private readonly IEventRepository _repository;

        public EventProcessor(ITransformHelper transformHelper, IEventRepository repository)
        {
            _transformHelper = transformHelper;
            _repository = repository;
        }

        public async Task<int> Insert(List<Event> events)
        {
            try
            {
                var entities = _transformHelper.Transform(events);

                return await _repository.InsertAsync(entities);
            }
            catch (Exception ex)
            {
                Logger.Error($"An error occured attempting to process collection of {nameof(Event)}");
                Logger.Error($"Class: {nameof(EventProcessor)}, Method: {nameof(Insert)}");
                Logger.Error(ex);
                throw;
            }
        }

        public async Task<int> AddOrUpdate(List<Event> events)
        {
            try
            {
                var entities = _transformHelper.Transform(events);

                return await _repository.AddOrUpdateAsync(entities);
            }
            catch (Exception ex)
            {
                Logger.Error($"An error occured attempting to process collection of {nameof(Event)}");
                Logger.Error($"Class: {nameof(EventProcessor)}, Method: {nameof(AddOrUpdate)}");
                Logger.Error(ex);
                throw;
            }
        }
    }
}