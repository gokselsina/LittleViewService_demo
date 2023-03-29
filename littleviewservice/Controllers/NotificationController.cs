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

        [HttpGet("Alarms/{send_to}")]
        public async Task<ActionResult<List<Notification>>> GetAlarms(int send_to)
        {
            var notiList = await _dbContext.tbl_notification
            .Where(n => (n.Send_to == send_to || n.Send_to == null) && n.Alarm)
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

        [HttpPost("addNotification")]
        public async Task<IActionResult> AddStudentAsync([FromBody] NotificationCredentials credentials)
        {

            Notification notification = new Notification
            {
                Text = credentials.Text,
                Send_from = credentials.Send_from,
                Send_to = credentials.Send_to,
                Send_date = credentials.Send_date,
                Unread = true,
            };

            _dbContext.tbl_notification.Add(notification);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
        }

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

        [HttpPut("markAsAlarmOff/{send_to}")]
        public async Task<IActionResult> UpdateNotificationAlarmAsync(int send_to)
        {
            var notifications = await _dbContext.tbl_notification.Where(n => n.Send_to == send_to).ToListAsync();
            if (notifications == null || notifications.Count == 0)
            {
                return NotFound();
            }
            foreach (var noti in notifications)
            {
                noti.Alarm = false;
                _dbContext.tbl_notification.Update(noti);
            }
            await _dbContext.SaveChangesAsync();
            return Ok("Updated!");
        }

        public class NotificationCredentials
        {
            public string Text { get; set; }
            public int Send_from { get; set; }
            public int? Send_to { get; set; }
            public DateTime? Send_date { get; set; } = DateTime.MinValue;
        }
    }
}
