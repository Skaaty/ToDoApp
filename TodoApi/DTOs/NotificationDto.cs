namespace TodoApi.DTOs
{
    public class NotificationDto
    {
        public record NotificationDTO(int Id, int UserId, string Message, bool IsReady);
        public record CreateNotificationDTO(int UserId, string Message);
    }
}
