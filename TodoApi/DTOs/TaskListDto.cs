namespace TodoApi.DTOs
{
    public class TaskListDto
    {
        public record TaskListDTO(int Id, string Name, string? Description, int UserId);
        public record CreateTaskListDTO(string Name, string? Description);
        public record UpdateTaskListDTO(string Name, string? Description);
    }
}
