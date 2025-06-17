namespace TodoApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public PriorityLevel? Priority { get; set; }
        public bool IsCompleted { get; set; }

        public int TaskListId { get; set; }
        public TaskList? TaskList { get; set; }

        public ICollection<TaskItemTag> TaskItemTags { get; set; } = [];
    }
}
