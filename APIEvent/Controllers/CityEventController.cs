using APIEvent.Core;
using APIEvent.Core.Interfaces;
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
        public async Task<ActionResult<List<CityEvent>>> GetEvents()
        {
            return Ok(await _cityEventService.GetEventsAsync());
        }

        [HttpGet("/CityEvent/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<CityEvent>>> GetEventByTitle(string title)
        {
            if (title == null)
            {
                return BadRequest();
            }

            var cityEvent = await _cityEventService.GetEventByTitleAsync(title);
            
            if (cityEvent.Count == 0)
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
        public async Task<ActionResult<List<CityEvent>>> GetEventByLocalDate(string local, DateTime? dateHourEvent)
        {
            if (local == null || dateHourEvent == null)
            {
                return BadRequest();
            }

            var cityEvent = await _cityEventService.GetEventByLocalDateAsync(local, dateHourEvent.Value);
            
            if (cityEvent.Count == 0)
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
        public async Task<ActionResult<List<CityEvent>>> GetEventByPriceRangeDateASync(decimal? min, decimal? max, DateTime? dateHourEvent)
        {
            if (!min.HasValue || !max.HasValue || !dateHourEvent.HasValue || min.Value > max.Value)
            {
                return BadRequest();
            }

            var cityEvent = await _cityEventService.GetEventByPriceRangeDateAsync(min.Value, max.Value, dateHourEvent.Value);
            
            if (cityEvent.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(cityEvent);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CityEvent>> PostEvent(CityEvent cityEvent)
        {
            if (!await _cityEventService.InsertEventAsync(cityEvent))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostEvent), cityEvent);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateEvent(long idEvent, CityEvent cityEvent)
        {
            if (!await _cityEventService.UpdateEventAsync(idEvent, cityEvent))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<CityEvent>>> DeleteEvent(long idEvent)
        {
            if (!await _cityEventService.DeleteEventAsync(idEvent))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}