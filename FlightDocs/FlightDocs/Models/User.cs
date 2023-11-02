using System.ComponentModel.DataAnnotations;

namespace FlightDocs.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Signature_Filepath { get; set; }

        //Foreign Connection
        public ICollection<Group> Groups { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<DocumentType> DocumentTypes { get; set; }
        public Account Account { get; set; }
    }
}
