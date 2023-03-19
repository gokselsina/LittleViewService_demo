using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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


        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login([FromBody] LoginRequest request)
        {
            var account = await _dbContext.tbl_account
                .FromSqlRaw("SELECT * FROM tbl_account WHERE Username = {0} COLLATE Latin1_General_CS_AS AND Password = {1} COLLATE Latin1_General_CS_AS", request.Username, request.Password)
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
