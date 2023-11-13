using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace FlightDocs.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created_date { get; set; }
        public string? Note { get; set; }

        //Foreign Connection
        public ICollection<Account> Accounts { get; set; }
        public ICollection<PermissionDG> PermissionDGs { get; set; }
        public ICollection<PermissionDTG> PermissionDTGs { get; set; }

        //Foreign Key
        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public User User { get; set; }
    }

    public class GroupInfo
    {
        public string Name { get; set; }
        //public DateTime Created_date { get; set; }
        public string? Note { get; set; }
        //public int CreatorId { get; set; }
    }
}
