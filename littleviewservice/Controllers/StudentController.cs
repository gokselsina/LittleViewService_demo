using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public StudentController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAccount()
        {
            if (_dbContext.tbl_student == null)
            {
                return NotFound();
            }
            return await _dbContext.tbl_student.ToListAsync();
        }
    }
}
