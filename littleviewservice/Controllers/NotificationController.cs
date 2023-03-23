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

        [HttpGet("{send_to}")]
        public async Task<ActionResult<List<Notification>>> GetNotification(int send_to)
        {
            var notiList = await _dbContext.tbl_notification
            .Where(n => n.Send_to == send_to || n.Send_to == null)
            .OrderByDescending(n => n.Send_date)  // Send_date sütununa göre tersine sırala
            .ThenByDescending(n => n.Unread) // Unread sütununa göre tersine sırala
            .ToListAsync();

            if (notiList == null || !notiList.Any())
            {
                return NotFound();
            }

            return notiList;
            
        }

        [HttpGet("countUnreadNotifications/{send_to}")]
        public async Task<IActionResult> CountUnreadNotificationsAsync(int send_to)
        {
            int count = await _dbContext.tbl_notification.CountAsync(n => n.Send_to == send_to && n.Unread);
            return Ok(count);
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

        [HttpPut("markAsRead/{send_to}")]
        public async Task<IActionResult> UpdateNotificationAsync(int send_to)
        {
            var notifications = await _dbContext.tbl_notification.Where(n => n.Send_to == send_to).ToListAsync();
            if (notifications == null || notifications.Count == 0)
            {
                return NotFound();
            }
            foreach (var noti in notifications)
            {
                noti.Unread = false;
                _dbContext.tbl_notification.Update(noti);
            }
            await _dbContext.SaveChangesAsync();
            return Ok("Updated!");
        }
    }
}
