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
    public class HomeworkController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public HomeworkController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Homework>>> GetActivity()
        {
            var actList = await _dbContext.tbl_homework
            .OrderBy(n => n.date)  // date sütununa göre tersine sırala
            .ToListAsync();

            if (actList == null || !actList.Any())
            {
                return NotFound();
            }

            return actList;

        }
        [HttpPost("addHomework")]
        public async Task<IActionResult> AddActivityAsync([FromBody] HomeworkCredentials credentials)
        {
            Homework hmw = new Homework
            {
                class_id = credentials.class_id,
                date = credentials.date,
                deadline = credentials.deadline,
                title = credentials.title,
                text = credentials.text
            };

            _dbContext.tbl_homework.Add(hmw);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
        }

        public class HomeworkCredentials
        {
            public DateTime? date { get; set; } = DateTime.MinValue;
            public DateTime? deadline { get; set; } = DateTime.MinValue;

            public int? class_id { get; set; }
            public string? title { get; set; }
            public string? text { get; set; }
        }

    }
}
