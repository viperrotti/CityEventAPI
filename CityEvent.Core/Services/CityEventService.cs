using APIEvent.Core.Interfaces;

namespace APIEvent.Core.Services
{
    public class CityEventService: ICityEventService
    {
        public ICityEventRepository _cityEventRepository;
        public CityEventService(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public List<CityEvent> GetEvents()
        {
            return _cityEventRepository.GetEvents();
        }

        public List<CityEvent> GetEventByTitle(string title)
        {
            return _cityEventRepository.GetEventByTitle(title);
        }

        public List<CityEvent> GetEventByLocalDate(string local, DateTime dateHourEvent)
        {
            return _cityEventRepository.GetEventByLocalDate(local, dateHourEvent);
        }

        public List<CityEvent> GetEventByPriceRangeDate(decimal min, decimal max, DateTime dateHourEvent)
        {
            return _cityEventRepository.GetEventByPriceRangeDate(min, max, dateHourEvent);
        }

        public bool InsertEvent(CityEvent cityEvent)
        {
            return _cityEventRepository.InsertEvent(cityEvent);
        }

        public bool DeleteEvent(long idEvent)
        {
            return _cityEventRepository.DeleteEvent(idEvent);
        }

        public bool UpdateEvent(long idEvent, CityEvent cityEvent)
        {
            return _cityEventRepository.UpdateEvent(idEvent, cityEvent);
        }
    }
}
