using System.ComponentModel.DataAnnotations;
using System.Text;

namespace littleviewservice.Models
{
    public class MyMessageList
    {
        public int? ID { get; set; }
        public string Text { get; set; }
        public int Send_from { get; set; }
        public string Send_from_name { get; set; }
        public int? Send_to { get; set; }
        public string Send_to_name { get; set; }
        public DateTime? Send_date { get; set; } = DateTime.MinValue; 
    }

    public class ChatMessageList
    {
        public int? ID { get; set; }
        public string Text { get; set; }
        public int Send_from { get; set; }
        public string Send_from_name { get; set; }
        public int? Send_to { get; set; }
        public DateTime? Send_date { get; set; } = DateTime.MinValue;
    }
}