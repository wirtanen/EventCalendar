using Events.Models;

namespace Events.Repo
{
    public interface ICalendarRepo
    {
        Task<List<CalendarEvent>> GetAllEventsForThisMonth(DateTime thisMonthsDate);
    }
}
