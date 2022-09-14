using System.ComponentModel.DataAnnotations;

namespace APIEvent.Core.Models
{
    public class EventReservation
    {
        public long IdReservation { get; set; }

        [Required(ErrorMessage = "Event ID is required")]
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public long Quantity { get; set; }

    }
}
