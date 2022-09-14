using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEvent.Core.Interfaces
{
    public interface ICityEventService
    {
        List<CityEvent> GetEvents();
        List<CityEvent> GetEventByTitle(string title);
        List<CityEvent> GetEventByLocalDate(string local, DateTime dateHourEvent);
        List<CityEvent> GetEventByPriceRangeDate(decimal min, decimal max, DateTime dateHourEvent);
        bool InsertEvent(CityEvent cityEvent);
        bool DeleteEvent(long idEvent);
        bool UpdateEvent(long idEvent, CityEvent cityEvent);
        bool CheckReservation(long idEvent);
        bool CheckStatus(long idEvent);
    }
}
