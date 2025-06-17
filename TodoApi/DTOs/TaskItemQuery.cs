using TodoApi.Models;

namespace TodoApi.DTOs
{
    public class TaskItemQuery
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
        public bool? IsCompleted { get; init; }
        public PriorityLevel? Priority { get; init; }
        public DateTime? DueDate { get; init; }
        public int? TaskId { get; init; }
    }
}
