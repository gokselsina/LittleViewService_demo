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
    public class ClassController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public ClassController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classes>>> GetClass()
        {
            var classList = await _dbContext.tbl_class
            .ToListAsync();

            if (classList == null || !classList.Any())
            {
                return NotFound();
            }

            return classList;

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Classes>> GetClassById(int id)
        {
            if (_dbContext.tbl_class == null)
            {
                return NotFound();
            }
            var cls = await _dbContext.tbl_class.FindAsync(id);
            if (cls == null)
            {
                return NotFound();
            }
            return cls;
        }

        [HttpPost("addClass")]
        public async Task<IActionResult> AddActivityAsync([FromBody] ClassCredentials credentials)
        {
            Classes cls = new Classes
            {
                name = credentials.name
            };

            _dbContext.tbl_class.Add(cls);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
        }

        public class ClassCredentials
        {
            public string? name { get; set; }
        }

    }
}
