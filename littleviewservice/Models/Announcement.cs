namespace littleviewservice.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        public DateTime? Send_date { get; set; }
        public int? Send_from { get; set; }
        public string? Send_from_name { get; set; }
        public string? Text { get; set; }
    }
}
