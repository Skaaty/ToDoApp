namespace TodoApi.DTOs
{
public record RegisterDto(string UserName, string Email, string Password);
public record LoginDto(string UserName, string Password);
}
