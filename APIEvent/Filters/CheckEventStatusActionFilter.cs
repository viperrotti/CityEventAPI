using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvent.Filters
{
    public class CheckEventStatusActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;

        public CheckEventStatusActionFilter(ICityEventService cityEnventService)
        {
            _cityEventService = cityEnventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            EventReservation eventReservation = (EventReservation)context.ActionArguments["eventReservation"];
            var eventSearch = _cityEventService.CheckStatus(eventReservation.IdEvent).Result;

            if (!eventSearch)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }

}

