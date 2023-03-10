using System.ComponentModel.DataAnnotations;

namespace littleviewservice.Models
{
    public class Student
    {
        public int ID { get; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public char Gender { get; set; }

    }
}
