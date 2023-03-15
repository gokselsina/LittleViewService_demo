using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class StudentImageController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public StudentImageController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentImage>>> GetStudent()
        {
            if (_dbContext.vw_profile_image == null)
            {
                return NotFound();
            }
            return await _dbContext.vw_profile_image.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentImage>> GetStudentImage(int id)
        {
            if (_dbContext.vw_profile_image == null)
            {
                return NotFound();
            }
            var image = await _dbContext.vw_profile_image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return image;
        }

        /**
        [HttpPost("addImage")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentCredentials credentials)
        {

            Student student = new Student
            {
                Name = credentials.name,
                Surname = credentials.surname,
                Gender = credentials.gender
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
        }**/
    }
}
