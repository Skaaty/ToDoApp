using TodoApi.Models;

namespace TodoApi.DTOs
{
        public record TaskItemDto(int Id, string Name, DateTime? DueDate,
            PriorityLevel? Priority, bool IsCompleted, int TaskListId,
            IEnumerable<string> Tags);

        public record CreateTaskItemDto(string Name, DateTime? DueDate,
            PriorityLevel? Priority, int TaskListId, IEnumerable<int> TagIds);

        public record UpdateTaskItemDto(string Name, DateTime? DueDate,
            PriorityLevel? Priority, IEnumerable<int> TagIds);
}
