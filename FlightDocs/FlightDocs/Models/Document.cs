using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        public string Version { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Updated_date { get; set; }
        public string File_path { get; set; }
        public string? Signature_Filepath { get; set; }

        //Foreign Connection
        public ICollection<OldVersion> OldVersions { get; set; }
        public ICollection<PermissionDG> PermissionDGs { get; set; }

        //Foreign Key
        public int DocumentTypeId { get; set; }
        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }

        public int FlightId { get; set; }
        [ForeignKey("FlightId")]
        public Flight Flight { get; set; }
    }
}
