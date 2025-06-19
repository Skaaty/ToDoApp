namespace TodoApi.DTOs
{
        public record NotificationDto(int Id, int UserId, string Message, bool IsReady);
        public record CreateNotificationDto(int UserId, string Message);
}
