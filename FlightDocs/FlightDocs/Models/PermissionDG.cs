using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs.Models
{
    public class PermissionDG
    {
        [Key]
        public int Id { get; set; }

        //Foreign Key
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public int DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
    }

    public class PermissionDGInfo
    {
        public int GroupId { get; set; }
        public int DocumentId { get; set; }
    }
}
