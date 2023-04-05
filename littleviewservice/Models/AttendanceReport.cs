using System.ComponentModel.DataAnnotations;
using System.Text;

namespace littleviewservice.Models
{
    public class AttendanceReport
    {
        [Key]
        public int Seq { get; set; }
        public int Student_id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
    }
}