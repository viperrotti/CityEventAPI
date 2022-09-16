using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEvent.Core.Interfaces
{
    public interface ICityEventService
    {
        Task<List<CityEvent>> GetEventsAsync();
        Task<List<CityEvent>> GetEventByTitleAsync(string title);
        Task<List<CityEvent>> GetEventByLocalDateAsync(string local, DateTime dateHourEvent);
        Task<List<CityEvent>> GetEventByPriceRangeDateAsync(decimal min, decimal max, DateTime dateHourEvent);
        Task<bool> InsertEventAsync(CityEvent cityEvent);
        Task<bool> UpdateEventAsync(long idEvent, CityEvent cityEvent);
        Task<bool> DeleteEventAsync(long idEvent);
        
        Task<bool> CheckReservation(long idEvent);
        Task<bool> CheckStatus(long idEvent);
    }
}
