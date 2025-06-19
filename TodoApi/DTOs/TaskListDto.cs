namespace TodoApi.DTOs
{

public record TaskListDto(int Id, string Name, string? Description, int UserId);
public record CreateTaskListDto(string Name, string? Description);
public record UpdateTaskListDto(string Name, string? Description);

}
