using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created_date { get; set; }

        //Foreign Connection
        public ICollection<PermissionDTG> PermissionDTGs { get; set; }
        public ICollection<Document> Documents { get; set; }

        //Foreign Key
        public int Customer_roleId { get; set; }
        [ForeignKey("Customer_roleId")]
        public Role Role { get; set; }

        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public User User { get; set; }
    }
}
