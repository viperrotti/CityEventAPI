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

        public async Task<List<CityEvent>> GetEventsAsync()
        {
            return await _cityEventRepository.GetEventsAsync();
        }

        public async Task<List<CityEvent>> GetEventByTitleAsync(string title)
        {
            return await _cityEventRepository.GetEventByTitleAsync(title);
        }

        public async Task<List<CityEvent>> GetEventByLocalDateAsync(string local, DateTime dateHourEvent)
        {
            return await _cityEventRepository.GetEventByLocalDateAsync(local, dateHourEvent);
        }

        public async Task<List<CityEvent>> GetEventByPriceRangeDateAsync(decimal min, decimal max, DateTime dateHourEvent)
        {
            return await _cityEventRepository.GetEventByPriceRangeDateAsync(min, max, dateHourEvent);
        }

        public async Task<bool> InsertEventAsync(CityEvent cityEvent)
        {
            return await _cityEventRepository.InsertEventAsync(cityEvent);
        }

        public async Task<bool> UpdateEventAsync(long idEvent, CityEvent cityEvent)
        {
            return await _cityEventRepository.UpdateEventAsync(idEvent, cityEvent);
        }

        public async Task<bool> DeleteEventAsync(long idEvent)
        {
            return await _cityEventRepository.DeleteEventAsync(idEvent);
        }

        public bool CheckReservation(long idEvent)
        {
            return _cityEventRepository.CheckReservation(idEvent);
        }

        public bool CheckStatus(long idEvent)
        {
            return _cityEventRepository.CheckStatus(idEvent);
        }
    }
}
