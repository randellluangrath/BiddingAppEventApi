using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using WebAppEventApi.Logging;
using WebAppEventApi.Models.Event;
using WebAppEventApi.Exceptions;

namespace WebAppEventApi.DataAccess
{
    public class EventRepository : IEventRepository
    {
        public async Task<int> InsertAsync(IEnumerable<EventEntity> events)
        {
            try
            {
                using (var dbContext = new DbContext())
                {
                    dbContext.Event.AddRange(events);

                    return await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var newException = HandleException(ex, nameof(InsertAsync));

                throw newException;
            }
        }

        public async Task<int> AddOrUpdateAsync(IEnumerable<EventEntity> events)
        {
            try
            {
                using (var dbContext = new DbContext())
                {
                    ProcessRecordsForUpdate(dbContext, events.ToList());

                    return await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var newException = HandleException(ex, nameof(AddOrUpdateAsync));

                throw newException;
            }
        }

        private void ProcessRecordsForUpdate(DbContext ctx, List<EventEntity> list)
        {
            var callIDList = list.Select(x => x.callID);
            var dbEvents = ctx.Event.Where(x => callIDList.Contains(x.callID));

            foreach (var evt in list)
            {
                var dbEvent = dbEvents.FirstOrDefault(x => x.callID == evt.callID);

                AddOrUpdate(ctx, dbEvent, evt);
            }
        }

        private void AddOrUpdate(DbContext dbContext, EventEntity dbEvent, EventEntity evt)
        {
            if (dbEvent == null)
            {
                dbContext.Event.Add(evt);
            }
            else
            {
                evt.ID = dbEvent.ID;

                dbContext.Entry(dbEvent).CurrentValues.SetValues(evt);
            }
        }

        private Exception HandleException(Exception ex, string method)
        {
            Logger.Error("An exception occurred while saving records to database.");
            Logger.Error($"Class: {nameof(EventRepository)}, Method: {method}");
            Logger.Error(ex);

            if (ex is DbUpdateException)
            {
                ((DbUpdateException)ex).Entries.ToList()
                    .ForEach(x => Logger.Error($"Exception occurred for callID: {((EventEntity)x.Entity).callID}"));

                var failedId = ((DbUpdateException)ex).Entries.Select(x => ((EventEntity)x.Entity).callID).FirstOrDefault();
                return new DbSaveException(failedId);

            }
            else
            {
                return ex;
            }
        }
    }

}
