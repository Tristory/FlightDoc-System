using System.ComponentModel.DataAnnotations;

namespace VSTT.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Picture { get; set; }
        public string? Language { get; set; }
    }
}
