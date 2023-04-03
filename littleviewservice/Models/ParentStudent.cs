using System.ComponentModel.DataAnnotations;
using System.Text;

namespace littleviewservice.Models
{
    public class ParentStudent
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public char? Gender { get; set; }
        public int? Parent_id { get; set; } = null;
        public int? Class_id { get; set; } = null;
        public string? Img { get; set; } = null;
    }
}