namespace APIEvent.Core.Interfaces
{
    public interface ICityEventRepository
    {
        Task<List<CityEvent>> GetEventsAsync();
        Task<List<CityEvent>> GetEventByTitleAsync(string title);
        Task<List<CityEvent>> GetEventByLocalDateAsync(string local, DateTime dateHourEvent);
        Task<List<CityEvent>> GetEventByPriceRangeDateAsync(decimal min, decimal max, DateTime dateHourEvent);
        Task<bool> InsertEventAsync(CityEvent cityEvent);
        Task<bool> UpdateEventAsync(long idEvent, CityEvent cityEvent);
        Task<bool> DeleteEventAsync(long idEvent);
        bool CheckReservation(long idEvent);
        bool CheckStatus(long idEvent);
    }
}
