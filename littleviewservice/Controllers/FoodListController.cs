using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class FoodListController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public FoodListController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetLesson()
        {
            var foodList = await _dbContext.tbl_food_list
            .ToListAsync();

            if (foodList == null || !foodList.Any())
            {
                return NotFound();
            }

            return foodList;
        }
    }
}
