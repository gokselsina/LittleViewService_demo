using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static littleviewservice.Controllers.NotificationController;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public AnnouncementController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        { 
            var announcementList = await _dbContext.tbl_announcement
            .OrderByDescending(n => n.Send_date)
            .ToListAsync();

            if (announcementList == null || !announcementList.Any())
            {
                return NotFound();
            }

            return announcementList;
        }

        [HttpPost("addAnnouncement")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AnnouncementCredentials credentials)
        {

            Announcement announcement = new Announcement
            {
                Text = credentials.Text,
                Send_from = credentials.Send_from,
                Send_from_name = credentials.Send_from_name,
                Send_date = credentials.Send_date
            };

            _dbContext.tbl_announcement.Add(announcement);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
        }
    }

    public class AnnouncementCredentials
    {
        public string Text { get; set; }
        public int Send_from { get; set; }
        public string Send_from_name { get; set; }
        public DateTime? Send_date { get; set; } = DateTime.MinValue;
    }
}
