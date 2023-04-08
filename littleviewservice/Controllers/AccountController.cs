using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static littleviewservice.Controllers.ActivityController;

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
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accountList = await _dbContext.tbl_account.ToListAsync();

            if (accountList == null || !accountList.Any())
            {
                return NotFound();
            }

            return accountList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {

            if (_dbContext.tbl_account == null)
            {
                return NotFound();
            }
            var account = await _dbContext.tbl_account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return account;

        }

        [HttpPost("addAccount")]
        public async Task<IActionResult> AddAccountAsync([FromBody] AccountCredentials credentials)
        {
            Account acc = new Account
            {
                username = credentials.Username,
                password = credentials.Password,
                name = credentials.Name,
                surname = credentials.Surname,
                account_type = credentials.Account_type
            };

            _dbContext.tbl_account.Add(acc);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
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

        public class AccountCredentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Account_type { get; set; }
        }
    }
}
