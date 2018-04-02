using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppEventApi.Models.Event;

namespace WebAppEventApi.DataAccess
{
    public interface IEventRepository
    {
        Task<int> InsertAsync(IEnumerable<EventEntity> list);
        Task<int> AddOrUpdateAsync(IEnumerable<EventEntity> list);
    }
}