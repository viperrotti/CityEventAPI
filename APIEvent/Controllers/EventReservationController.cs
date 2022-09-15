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
        public async Task<ActionResult<List<EventReservation>>> GetReservations()
        {
            return Ok(await _eventReservationService.GetReservationsAsync());
        }

        [HttpGet("/EventReservation/TitleAndPersonName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EventReservation>>> GetReservationByTitleAndPersonName(string personName, string title)
        {
            var cityEvent = await _eventReservationService.GetReservationByTitleAndPersonNameAsync(personName, title);
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
        public async Task<ActionResult<EventReservation>> PostReservation(EventReservation eventReservation)
        {
            if (!await _eventReservationService.InsertReservationAsync(eventReservation))
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
        public async Task<IActionResult> UpdateReservationQuantity(long idReservation, int quantity)
        {
            if (!await _eventReservationService.UpdateReservationQuantityAsync(idReservation, quantity))
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
        public async Task<ActionResult<List<EventReservation>>> DeleteReservation(long idReservation)
        {
            if (!await _eventReservationService.DeleteReservationAsync(idReservation))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
