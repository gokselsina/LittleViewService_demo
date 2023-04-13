using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static littleviewservice.Controllers.MessageController;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public MessageController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyMessageList>>> GetMessages()
        {
            if (_dbContext.view_my_messages == null)
            {
                return NotFound();
            }
            return await _dbContext.view_my_messages.ToListAsync();
        }

        [HttpGet("{send_to}")]
        public async Task<ActionResult<List<MyMessageList>>> GetMessagesBySendTo(int send_to)
        {
            var msgList = await _dbContext.view_my_messages
            .Where(n => n.Send_to == send_to || n.Send_to == null )
            .OrderByDescending(n => n.Send_date)  // Send_date sütununa göre tersine sırala
            //.ThenByDescending(n => n.Unread) // Unread sütununa göre tersine sırala
            .ToListAsync();

            if (msgList == null || !msgList.Any())
            {
                return NotFound();
            }

            return msgList;
            
        }

        [HttpGet("{send_from}/{send_to}")]
        public async Task<ActionResult<List<ChatMessageList>>> GetChat(int send_from, int send_to)
        {
            var msgList = await _dbContext.tbl_message
            .Where(n => (n.Send_to == send_to && n.Send_from == send_from) || (n.Send_to == send_from && n.Send_from == send_to))
            .OrderBy(n => n.Send_date)  // Send_date sütununa göre tersine sırala
            //.ThenByDescending(n => n.Unread) // Unread sütununa göre tersine sırala
            .ToListAsync();

            if (msgList == null || !msgList.Any())
            {
                return NotFound();
            }

            return msgList;

        }

        /**
        [HttpGet("countUnreadMessages/{send_to}")]
        public async Task<IActionResult> CountUnreadMessagesAsync(int send_to)
        {
            int count = await _dbContext.tbl_message.CountAsync(n => n.Send_to == send_to && n.Unread);
            return Ok(count);
        }

       

        
        [HttpGet("Alarms/{send_to}")]
        public async Task<ActionResult<List<Message>>> GetAlarms(int send_to)
        {
            var msgList = await _dbContext.tbl_message
            .Where(n => (n.Send_to == send_to || n.Send_to == null) && n.Alarm)
            .OrderByDescending(n => n.Send_date)  // Send_date sütununa göre tersine sırala
            .ThenByDescending(n => n.Unread) // Unread sütununa göre tersine sırala
            .ToListAsync();

            if (msgList == null || !msgList.Any())
            {
                return NotFound();
            }

            return msgList;

        }
 
        [HttpPut("markAsRead/{send_to}/{send_from}")]
        public async Task<IActionResult> UpdateMessageAsync(int send_to, int send_from)
        {
            var msgList = await _dbContext.tbl_message.Where(n => n.Send_to == send_to && n.Send_from == send_from).ToListAsync();
            if (msgList == null || msgList.Count == 0)
            {
                return NotFound();
            }
            foreach (var msg in msgList)
            {
                msg.Unread = false;
                _dbContext.tbl_message.Update(msg);
            }
            await _dbContext.SaveChangesAsync();
            return Ok("Updated!");
        }

        [HttpPut("markAsAlarmOff/{send_to}")]
        public async Task<IActionResult> UpdateMessageAlarmAsync(int send_to)
        {
            var msgList = await _dbContext.tbl_message.Where(n => n.Send_to == send_to).ToListAsync();
            if (msgList == null || msgList.Count == 0)
            {
                return NotFound();
            }
            foreach (var msg in msgList)
            {
                msg.Alarm = false;
                _dbContext.tbl_message.Update(msg);
            }
            await _dbContext.SaveChangesAsync();
            return Ok("Updated!");
        }**/

        public class MessageCredentials
        {
            public string Text { get; set; }
            public int Send_from { get; set; }
            public int? Send_to { get; set; }
            public DateTime? Send_date { get; set; } = DateTime.MinValue;
        }
    }
}
