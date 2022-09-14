using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;
using APIEvent.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIEvent.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/EventReservations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<EventReservation>> GetReservations()
        {
            return Ok(_eventReservationService.GetReservations());
        }

        [HttpGet("/EventReservation/TitleAndPersonName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<EventReservation>> GetReservationByTitleAndPersonName(string personName, string title)
        {
            var cityEvent = _eventReservationService.GetReservationByTitleAndPersonName(personName, title);
            if (cityEvent.Count == 0)
            {
                return NotFound();
            }
            return Ok(cityEvent);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(CheckEventStatusActionFilter))]
        public ActionResult<EventReservation> PostReservation(EventReservation eventReservation)
        {
            if (!_eventReservationService.InsertReservation(eventReservation))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(PostReservation), eventReservation);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateReservationQuantity(long idReservation, int quantity)
        {
            if (!_eventReservationService.UpdateReservationQuantity(idReservation, quantity))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public ActionResult<List<EventReservation>> DeleteReservation(long idReservation)
        {
            if (!_eventReservationService.DeleteReservation(idReservation))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
