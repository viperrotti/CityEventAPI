using APIEvent.Core;
using APIEvent.Core.Interfaces;
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
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/CityEvents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetEvents()
        {
            return Ok(_cityEventService.GetEvents());
        }


        [HttpGet("/CityEvent/Title")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetEventByTitle(string title)
        {
            var cityEvent = _cityEventService.GetEventByTitle(title);
            if (cityEvent == null)
            {
                return NotFound();
            }
            return Ok(cityEvent);
        }

        [HttpGet("/CityEvent/LocalAndDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetEventByLocalDate(string local, DateTime dateHourEvent)
        {
            var cityEvent = _cityEventService.GetEventByLocalDate(local, dateHourEvent);
            if (cityEvent == null)
            {
                return NotFound();
            }
            return Ok(cityEvent);
        }

        [HttpGet("/CityEvent/PriceRangeAndDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetEventByPriceRangeDate(decimal min, decimal max, DateTime dateHourEvent)
        {
            var cityEvent = _cityEventService.GetEventByPriceRangeDate(min, max, dateHourEvent);
            if (cityEvent == null)
            {
                return NotFound();
            }
            return Ok(cityEvent);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public ActionResult<CityEvent> PostEvent(CityEvent cityEvent)
        {
            if (!_cityEventService.InsertEvent(cityEvent))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(PostEvent), cityEvent);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateEvent(long idEvent, CityEvent cityEvent)
        {
            if (!_cityEventService.UpdateEvent(idEvent, cityEvent))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public ActionResult<List<CityEvent>> DeleteEvent(long idEvent)
        {
            if (!_cityEventService.DeleteEvent(idEvent))
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}