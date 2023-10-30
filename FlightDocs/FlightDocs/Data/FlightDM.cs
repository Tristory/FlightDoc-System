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
