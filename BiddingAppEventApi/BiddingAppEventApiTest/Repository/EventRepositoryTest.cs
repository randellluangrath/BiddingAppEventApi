using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Fakes;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebAppEventApi.DataAccess;
using WebAppEventApi.Models.Event;
using WebAppEventApiTest.Helpers;
using WebAppEventApi.Exceptions;
using Xunit;

namespace WebAppEventApiTest.Repository
{
    public class EventRepositoryTest : TestBase<EventRepository>
    {
        public EventRepositoryTest()
        {
            InstantiateClassUnderTest();
        }

        [Fact]
        public async Task EventRepository_Insert_WithRecordsThatDoNotExistInDb_CreatesRecords()
        {
            var SaveChangesAsyncWasCalled = false;

            ShimDbContext.AllInstances.SaveChangesAsync = (x) =>
            {
                SaveChangesAsyncWasCalled = true;
                int task = Convert.ToInt32(SaveChangesAsyncWasCalled);
                return Task.FromResult(task);
            };

            var list = new List<EventEntity>(EventHelper.GetEventEntityList());

            await ClassUnderTest.InsertAsync(list);
            Assert.True(SaveChangesAsyncWasCalled);
        }

        [Fact]
        public void EventRepository_Insert_WithRecordsThatDoExistInDb_ThrowsException()
        {
            var SaveChangesAsyncWasCalled = false;

            ShimDbContext.AllInstances.SaveChangesAsync = (x) =>
            {
                SaveChangesAsyncWasCalled = true;
                throw new DbUpdateException();
            };

            var list = new List<EventEntity>(EventHelper.GetEventEntityList());

            Assert.ThrowsAsync<DbSaveException>(async () => await ClassUnderTest.InsertAsync(list));
            Assert.True(SaveChangesAsyncWasCalled);
        }

        [Fact]
        public async Task EventRepository_AddOrUpdate_WithRecordsThatDoAndDoNotExistInDb_CreatesRecords()
        {
            var SaveChangesAsyncWasCalled = false;

            ShimDbContext.AllInstances.SaveChangesAsync = (x) =>
            {
                SaveChangesAsyncWasCalled = true;
                int task = Convert.ToInt32(SaveChangesAsyncWasCalled);
                return Task.FromResult(task);
            };

            var list = new List<EventEntity>(EventHelper.GetEventEntityList());

            await ClassUnderTest.AddOrUpdateAsync(list);
            Assert.True(SaveChangesAsyncWasCalled);
        }

    }
}
