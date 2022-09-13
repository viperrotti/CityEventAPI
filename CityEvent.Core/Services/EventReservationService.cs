using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;

namespace APIEvent.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;
        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public List<EventReservation> GetReservations()
        {
            return _eventReservationRepository.GetReservations();
        }

        public List<EventReservation> GetReservationByTitleAndPersonName(string personName, string title)
        {
            return _eventReservationRepository.GetReservationByTitleAndPersonName(personName, title);
        }

        public bool InsertReservation(EventReservation eventReservation)
        {
            return _eventReservationRepository.InsertReservation(eventReservation);
        }

        public bool UpdateReservationQuantity(long idReservation, int quantity)
        {
            return _eventReservationRepository.UpdateReservationQuantity(idReservation, quantity);
        }

        public bool DeleteReservation(long idReservation)
        {
            return _eventReservationRepository.DeleteReservation(idReservation);
        }



        
    }
}
