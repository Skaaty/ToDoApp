namespace TodoApi.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int UserId { get; set; }

        public ICollection<TaskItem> Items { get; set; } = [];
    }
}
