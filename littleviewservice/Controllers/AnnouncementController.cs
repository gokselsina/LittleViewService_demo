using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            .ToListAsync();

            if (announcementList == null || !announcementList.Any())
            {
                return NotFound();
            }

            return announcementList;
        }
    }
}
