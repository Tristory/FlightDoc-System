using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs.Models
{
    public class PermissionDTG
    {
        public int Id { get; set; }
        public int Access_Level { get; set; }

        //Foreign Key
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public int DocumentTypeId { get; set; }
        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }
    }
}
