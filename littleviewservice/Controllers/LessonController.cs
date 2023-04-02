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
    public class LessonController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public LessonController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLesson()
        {
            var lessonList = await _dbContext.tbl_weekly_program
            .ToListAsync();

            if (lessonList == null || !lessonList.Any())
            {
                return NotFound();
            }

            return lessonList;

        }

        [HttpGet("{class_id}")]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetClassLesson(int class_id)
        {
            var lessonList = await _dbContext.tbl_weekly_program.Where(s => s.Class_id == class_id).ToListAsync();
            if (lessonList == null || !lessonList.Any())
            {
                return NotFound();
            }

            return lessonList;

        }
        /**
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
        }**/

        public class LessonCredentials
        {
            public string? name { get; set; }
            public int? sequence { get; set; }
        }

    }
}
