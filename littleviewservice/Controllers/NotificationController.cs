using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static littleviewservice.Controllers.NotificationController;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public NotificationController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotification()
        {
            if (_dbContext.tbl_notification == null)
            {
                return NotFound();
            }
            return await _dbContext.tbl_notification.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            if (_dbContext.tbl_notification == null)
            {
                return NotFound();
            }
            var noti = await _dbContext.tbl_notification.FindAsync(id);
            if (noti == null)
            {
                return NotFound();
            }
            return noti;
        }
        /**
        [HttpPost("addNotification")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentCredentials credentials)
        {
            
            StudentProfile Notification = new StudentProfile
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
        **/
        /**
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
        **/
        public class NotifcationCredentials
        {
            public int? ID { get; set; }
            public string Text { get; set; }
            public int Send_from { get; set; }
            public int? Send_to { get; set; }
            public DateTime? Send_date { get; set; } = DateTime.MinValue;
            public bool Unread { get; set; }
        }
    }
}
