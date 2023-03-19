using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            if (_dbContext.tbl_activity == null)
            {
                return NotFound();
            }
            return await _dbContext.tbl_activity.ToListAsync();
        }
    }
}
