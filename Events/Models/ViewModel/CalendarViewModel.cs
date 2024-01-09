namespace Events.Models
{
    public class CalendarViewModel
    {
        public List<CalendarEvent> EventsThisMonth = new List<CalendarEvent>();
        public DateTime ThisMonth { get; set; }
    }
}
