using APIEvent.Core.Models;

namespace APIEvent.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<EventReservation> GetReservations();
        List<EventReservation> GetReservationByTitleAndPersonName(string personName, string title);
        bool InsertReservation(EventReservation eventReservation);
        bool UpdateReservationQuantity(long idReservation, int quantity);
        bool DeleteReservation(long idReservation);
        
    }
}
