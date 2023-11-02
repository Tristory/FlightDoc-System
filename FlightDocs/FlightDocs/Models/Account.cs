using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }

        //Foreign Key
        public int RoleId {  get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

    }
}
