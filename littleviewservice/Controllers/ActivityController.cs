using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using static littleviewservice.Controllers.StudentController;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public ActivityController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivity()
        {
            var actList = await _dbContext.tbl_activity
                .Where(n => n.date >= DateTime.Today)
            .OrderBy(n => n.date)  // date sütununa göre tersine sırala
            .ToListAsync();

            if (actList == null || !actList.Any())
            {
                return NotFound();
            }

            return actList;

        }
        [HttpPost("addActivity")]
        public async Task<IActionResult> AddActivityAsync([FromBody] ActivityCredentials credentials)
        {
            Activity act = new Activity
            {
                date = credentials.date,
                title = credentials.title,
                text = credentials.text
            };

            _dbContext.tbl_activity.Add(act);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
        }

        public class ActivityCredentials
        {
            public DateTime? date { get; set; } = DateTime.MinValue;
            public string? title { get; set; }
            public string? text { get; set; }
        }

    }
}
