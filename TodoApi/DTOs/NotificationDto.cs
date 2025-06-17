namespace TodoApi.DTOs
{
    public class NotificationDto
    {
        public record NotificationDTO(int Id, int UserId, string Message, bool IsReady);
        public record CreateNotification(int UserId, string Message);
    }
}
