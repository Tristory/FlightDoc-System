using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs.Models
{
    public class OldVersion
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string File_path { get; set; }
        public string Signature_Filepath { get; set; }

        //Foreign Key
        public int DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
    }
}
