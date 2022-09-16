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

        public async Task<List<EventReservation>> GetReservationsAsync()
        {
            return await _eventReservationRepository.GetReservationsAsync();
        }

        public async Task<List<EventReservation>> GetReservationByTitleAndPersonNameAsync(string personName, string title)
        {
            return await _eventReservationRepository.GetReservationByTitleAndPersonNameAsync(personName, title);
        }

        public async Task<bool> InsertReservationAsync(EventReservation eventReservation)
        {
            return await _eventReservationRepository.InsertReservationAsync(eventReservation);
        }

        public async Task<bool> UpdateReservationQuantityAsync(long idReservation, int quantity)
        {
            return await _eventReservationRepository.UpdateReservationQuantityAsync(idReservation, quantity);
        }

        public async Task<bool> DeleteReservationAsync(long idReservation)
        {
            return await _eventReservationRepository.DeleteReservationAsync(idReservation);
        }

    }
}
