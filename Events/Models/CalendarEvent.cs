namespace Events.Models
{
    public class CalendarEvent
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; } = "Event Name";
        public string? Description { get; set; }
        public string? Location { get; set; }

        public DateTime AlertTime { get; set; }
    }
}
