using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_TEST.Data
{
    public class Format
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FormatType { get; set; }
        public ICollection<Recording> Recordings { get; set; }
    }
}
