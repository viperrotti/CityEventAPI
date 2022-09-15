using APIEvent.Core.Models;

namespace APIEvent.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        Task<List<EventReservation>> GetReservationsAsync();
        Task<List<EventReservation>> GetReservationByTitleAndPersonNameAsync(string personName, string title);
        Task<bool> InsertReservationAsync(EventReservation eventReservation);
        Task<bool> UpdateReservationQuantityAsync(long idReservation, int quantity);
        Task<bool> DeleteReservationAsync(long idReservation);
        
    }
}
