using System.ComponentModel.DataAnnotations;
using System.Text;

namespace littleviewservice.Models
{
    public class StudentProfile
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public char? Gender { get; set; }
        public string? Blood_type { get; set; }
        public DateTime? Birth_date { get; set; } = DateTime.MinValue;
        public string? Parent_1 { get; set; } = null;
        public string? Parent_number_1 { get; set; } = null;
        public string? Parent_2 { get; set; } = null;
        public string? Parent_number_2 { get; set; } = null;
        public string? Address { get; set; } = null;
        public string? Notes { get; set; } = null;
        public string? Img { get; set; } = null;
        public int? Parent_id { get; set; }
    }
}