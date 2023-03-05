using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class LittleController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public LittleController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Little>>> GetLittles()
        {
            if(_dbContext.Littles == null)
            {
                return NotFound();  
            }
            return await _dbContext.Littles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Little>> GetLittle(int id)
        {
            if (_dbContext.Littles == null)
            {
                return NotFound();
            }
            var little = await _dbContext.Littles.FindAsync(id);
            if(little == null)
            {
                return NotFound();
            }
            return little;
        }

        [HttpPost]
        public async Task<ActionResult<Little>> PostLittle(Little little)
        {
            _dbContext.Littles.Add(little);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLittle), new {id = little.ID}, little);
        }
    }
}
