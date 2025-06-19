namespace TodoApi.DTOs
{
        public record NotificationDto(
            int Id,
            int TaskItemId,
            DateTime FireAtUtc,
            string? Message
            );
        public record CreateNotificationDto(
            int TaskItemId,
            DateTime FireAtUtc,
            string? Message
            );
}
