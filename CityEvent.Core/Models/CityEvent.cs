using System.ComponentModel.DataAnnotations;

namespace APIEvent.Core
{
    public class CityEvent
    {
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Event Title is required")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Event Date is required")]
        public DateTime? DateHourEvent { get; set; }

        [Required(ErrorMessage = "Event Local is required")]
        public string? Local { get; set; }

        public string? Address { get; set; }

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }

    }
}