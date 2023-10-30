using System.ComponentModel.DataAnnotations;

namespace FlightDocs.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //Foreign Connection
        public ICollection<DocumentType> DocumentTypes { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
