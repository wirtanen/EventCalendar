using Events.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;


namespace Events.Repo
{
    public class CalendarRepo : ICalendarRepo
    {
        private readonly string _connectionString;

        public CalendarRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Method to get all events at a given month.
        /// 
        /// ...maybe 2 params fromDate and toDate (1st to endOfMonth) would make the sproc query a bit less...messy
        /// </summary>
        /// <param name="thisMonthsDate">month to query</param>
        /// <returns>List<CalendarEvent></returns>
        public async Task<List<CalendarEvent>> GetAllEventsForThisMonth(DateTime thisMonthsDate)
        {
            var results = new List<CalendarEvent>();

            /// Stored Procedure notes
            /// Events.GetEventsThisMonth
            /// SELECT StartDateTime, EndDateTime, Title, Location, Description from Event.Events
            /// WHERE (MONTH(StartDateTime) = MONTH(@ThisMonthsDate) AND YEAR(StartDateTime) = YEAR(@ThisMonthsDate)) 
            /// OR (MONTH(EndDateTime) = MONTH(@ThisMonthsDate) AND YEAR(EndDateTime) = YEAR(@ThisMonthsDate))
            /// OR Start and End dates enclose the given date


            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("Events.GetEventsThisMonth", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ThisMonthsDate", thisMonthsDate);
                    conn.Open();

                    using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (reader.Read())
                        {
                            results.Add(new CalendarEvent
                            {
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                Description = reader.GetString("Description"),
                                Title = reader.GetString("Title"),
                                Location = reader.GetString("Location")
                                //AlertTime
                            });
                        }
                    }

                    conn.Close();
                }
            }

            return results;
        }
    }


}
