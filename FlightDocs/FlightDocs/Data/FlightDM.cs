using FlightDocs.Models;

namespace FlightDocs.Data
{
    public class FlightDM
    {
        private readonly ApplicationDbContext _context;

        public FlightDM(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Flight> GetFlights() => _context.Flights.ToList();

        public List<Flight> GetCurrentFlights()
        {
            DateTime today = DateTime.Now;

            return _context.Flights
                .Where(e => e.Departure_Time < today && e.Arrival_Time > today)
                .ToList();
        }

        public List<Flight> GetIncomingFlight()
        {
            DateTime today = DateTime.Now;

            return _context.Flights
                .Where(e => e.Departure_Time > today)
                .ToList();
        }

        public List<Flight> GetTodayFlights()
        {
            DateTime today = DateTime.Now;

            return _context.Flights
                .Where(e => e.Departure_Time.Date == today.Date)
                .ToList();
        }

        public string AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
            return "Add success!";
        }

        public string UpdateFlight(Flight flight)
        {
            _context.Flights.Update(flight);
            _context.SaveChanges();
            return "Update success!";
        }
    }
}
