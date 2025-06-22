using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TodoApi.Data;
using TodoApi.Services;

namespace TodoApi.Workers
{
    public class NotificationDispatcher : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<NotificationDispatcher> _logger;

        public NotificationDispatcher(IServiceScopeFactory scopeFactory, ILogger<NotificationDispatcher> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stop)
        {
            while (!stop.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<TodoContext>();
                var email = scope.ServiceProvider.GetRequiredService<IEmailSender>(); // scoped

                var due = await ctx.Notifications
                                   .Include(n => n.TaskItem.TaskList)
                                   .Where(n => !n.Sent && n.FireAtUtc <= DateTime.UtcNow)
                                   .ToListAsync(stop);

                _logger.LogDebug("Dispatcher checked at {UtcNow}. Due count = {Count}",
                                 DateTime.UtcNow, due.Count);

                foreach (var n in due)
                {
                    var body = string.IsNullOrWhiteSpace(n.Message) ? $"Task \"{n.TaskItem.Name}\" is due to be completed." : n.Message;

                    var to = await ctx.Users
                                      .Where(u => u.Id == n.TaskItem.TaskList!.UserId)
                                      .Select(u => u.Email)
                                      .FirstAsync(stop);

                    await email.SendAsync(to, "Notification about the task", body, stop);

                    n.Sent = true;
                    _logger.LogInformation("Notification {Id} | Task: \"{TaskName}\" | To: {Email} | Body: \"{Body}\"", n.Id, n.TaskItem.Name, to, body);
                }

                await ctx.SaveChangesAsync(stop);
                await Task.Delay(TimeSpan.FromMinutes(1), stop);
            }
        }
    }
}

