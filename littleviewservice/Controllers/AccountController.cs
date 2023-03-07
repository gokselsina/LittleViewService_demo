using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public AccountController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccount()
        {
            if (_dbContext.tbl_account == null)
            {
                return NotFound();
            }
            return await _dbContext.tbl_account.ToListAsync();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            var user = _dbContext.tbl_account.SingleOrDefault(a => a.username == credentials.username);

            if (user == null || user.password != credentials.password)
            {
                return BadRequest("Invalid username or password");
            }
            return Ok(user.account_type);

        }
        public class UserCredentials
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
