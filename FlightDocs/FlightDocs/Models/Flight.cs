using System.ComponentModel.DataAnnotations;

namespace FlightDocs.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public DateTime Departure_Time { get; set; }
        public DateTime Arrival_Time { get; set; }
        public string From_Location { get; set; }
        public string To_Location { get; set; }
        public string AircratNumber { get; set; }

        //Foreign Connection
        public ICollection<Document> Documents { get; set; }
    }
}
