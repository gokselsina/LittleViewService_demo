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
            if (_dbContext.tbl_student == null)
            {
                return NotFound();
            }
            return await _dbContext.tbl_student.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_dbContext.tbl_student == null)
            {
                return NotFound();
            }
            var student = await _dbContext.tbl_student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost("addStudent")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentCredentials credentials)
        {
            
            Student student = new Student
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
                Notes = credentials.notes
            };
            
            if (student.Gender != 'M' &&
                student.Gender != 'F' &&
                student.Gender != 'E' &&
                student.Gender != 'K') return NotFound();

            _dbContext.tbl_student.Add(student);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
        }

        public class StudentCredentials
        {
            public string name { get; set; }
            public string surname { get; set; }
            public char gender { get; set; }
            public string blood_type { get; set; } = null;
            public DateTime birth_date { get; set; } = DateTime.MinValue;
            public string parent_1 { get; set; } = null;
            public string parent_number_1 { get; set; } = null;
            public string parent_2 { get; set; } = null;
            public string parent_number_2 { get; set; } = null;
            public string address { get; set; } = null;
            public string notes { get; set; } = null;
        }
    }
}
