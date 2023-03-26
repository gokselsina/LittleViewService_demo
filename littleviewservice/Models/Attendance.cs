using System.ComponentModel.DataAnnotations;
using System.Text;

namespace littleviewservice.Models
{
    public class Attendance
    {
        public int? ID { get; set; }
        public int Student_id { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
    }
}