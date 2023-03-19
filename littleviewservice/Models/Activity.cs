namespace littleviewservice.Models
{
    public class Activity
    {
        public int ID { get; set; }
        public DateTime? date { get; set; } = DateTime.MinValue;
        public string? title { get; set; }
        public string? text { get; set; }
    }
}
