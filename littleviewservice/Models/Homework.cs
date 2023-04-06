namespace littleviewservice.Models
{
    public class Homework
    {
        public int ID { get; set; }
        public int? class_id { get; set; }
        public DateTime? date { get; set; } = DateTime.MinValue;
        public DateTime? deadline { get; set; } = DateTime.MinValue;
        public string? title { get; set; }
        public string? text { get; set; }
    }
}
