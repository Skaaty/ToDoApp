namespace TodoApi.DTOs
{
    public class TagDto
    {
        public record TagDTO(int Id, string Name);
        public record CreateTagDTO(string Name);
        public record UpdateTagDTO(string Name);
    }
}
