namespace TodoApi.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int TaskItemId { get; set; }
        public DateTime FireAtUtc { get; set; }
        public string? Message { get; set; }

        public bool Sent { get; set; }
        public TaskItem TaskItem { get; set; } = null!;
    }
}
