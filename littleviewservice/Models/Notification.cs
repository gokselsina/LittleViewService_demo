using System.ComponentModel.DataAnnotations;
using System.Text;

namespace littleviewservice.Models
{
    public class Notification
    {
        public int? ID { get; set; }
        public string Text { get; set; }
        public int Send_from { get; set; }
        public int? Send_to { get; set; }
        public DateTime? Send_date { get; set; } = DateTime.MinValue; 
        public bool Unread { get; set; }
        public bool Alarm { get; set; }
    }
}