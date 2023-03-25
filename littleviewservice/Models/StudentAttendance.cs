using System.ComponentModel.DataAnnotations;
using System.Text;

namespace littleviewservice.Models
{
    public class StudentAttendance
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}