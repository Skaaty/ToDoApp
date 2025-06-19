namespace TodoApi.Workers
{
    public class ConsoleEmailSender : IEmailSender
    {
        public Task SendAsync(string to, string subject, string body, CancellationToken ct = default)
        {
            Console.WriteLine($" EMAIL  to={to}\n   {subject}\n   {body}");
            return Task.CompletedTask;
        }
    }
}
