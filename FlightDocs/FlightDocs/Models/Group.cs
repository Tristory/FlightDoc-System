using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created_date { get; set; }
        public string Note { get; set; }

        //Foreign Connection
        public ICollection<Account> Accounts { get; set; }
        public ICollection<PermissionDG> PermissionDGs { get; set; }
        public ICollection<PermissionDTG> PermissionDTGs { get; set; }

        //Foreign Key
        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public User User { get; set; }
    }
}
