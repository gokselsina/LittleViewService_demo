using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static littleviewservice.Controllers.AccountController;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public StudentController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            if (_dbContext.vw_students == null)
            {
                return NotFound();
            }
            return await _dbContext.vw_students.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_dbContext.vw_students == null)
            {
                return NotFound();
            }
            var student = await _dbContext.vw_students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost("addStudent")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentCredentials credentials)
        {
            
            StudentProfile student = new StudentProfile
            {
                Name = credentials.name,
                Surname = credentials.surname,
                Gender = credentials.gender,
                Blood_type = credentials.blood_type,
                Birth_date = credentials.birth_date,
                Parent_1 = credentials.parent_1,
                Parent_number_1 = credentials.parent_1,
                Parent_2= credentials.parent_2,
                Parent_number_2 = credentials.parent_2,
                Address = credentials.address,
                Notes = credentials.notes,
                Img = credentials.img
            };
            
            if (student.Gender != 'M' &&
                student.Gender != 'F' &&
                student.Gender != 'E' &&
                student.Gender != 'K') return NotFound();

            _dbContext.tbl_student.Add(student);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
        }

        [HttpPut("updateStudent/{id}")]
        public async Task<IActionResult> UpdateStudentAsync(int id, [FromBody] StudentCredentials credentials)
        {
            var student = await _dbContext.tbl_student.FindAsync(id);
            if (student == null) return NotFound();

            student.Name = credentials.name;
            student.Surname = credentials.surname;
            student.Gender = credentials.gender;
            student.Blood_type = credentials.blood_type;
            student.Birth_date = credentials.birth_date;
            student.Parent_1 = credentials.parent_1;
            student.Parent_number_1 = credentials.parent_number_1;
            student.Parent_2 = credentials.parent_2;
            student.Parent_number_2 = credentials.parent_number_2;
            student.Address = credentials.address;
            student.Notes = credentials.notes;
            student.Img = credentials.img;

            if (student.Gender != 'M' &&
                student.Gender != 'F' &&
                student.Gender != 'E' &&
                student.Gender != 'K') return NotFound();

            _dbContext.tbl_student.Update(student);
            await _dbContext.SaveChangesAsync();
            return Ok("Updated!");
        }

        public class StudentCredentials
        {
            public string name { get; set; }
            public string surname { get; set; }
            public char gender { get; set; }
            public string blood_type { get; set; } = "";
            public DateTime birth_date { get; set; } = DateTime.MinValue;
            public string parent_1 { get; set; } = "";
            public string parent_number_1 { get; set; } = "";
            public string parent_2 { get; set; } = "";
            public string parent_number_2 { get; set; } = "";
            public string address { get; set; } = "";
            public string notes { get; set; } = "";
            public string img { get; set; } = "";
        }
    }
}
