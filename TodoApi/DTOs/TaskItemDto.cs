using TodoApi.Models;

namespace TodoApi.DTOs
{
    public class TaskItemDto
    {
        public record TaskItemDTO(int Id, string Name, DateTime? DueDate,
            PriorityLevel? Priority, bool IsCompleted, int TaskListId,
            IEnumerable<string> Tags);

        public record CreateTaskItemDTO(string Name, DateTime? DueDate,
            PriorityLevel? Priority, int TaskListId, IEnumerable<int> TagIds);

        public record UpdateTaskItemDTO(string Name, DateTime? DueDate,
            PriorityLevel? Priority, IEnumerable<int> TagIds);
    }
}
