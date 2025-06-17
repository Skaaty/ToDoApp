using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<TaskList> TaskLists => Set<TaskList>();
        public DbSet<TaskItem> TaskItems => Set<TaskItem>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            b.Entity<TaskItemTag>().HasKey(t => new { t.TaskItemId, t.TagId });
            b.Entity<TaskItemTag>()
                .HasOne(x => x.TaskItem).WithMany(x => x.TaskItemTags).HasForeignKey(x => x.TaskItemId);
            b.Entity<TaskItemTag>()
                .HasOne(x => x.Tag).WithMany(x => x.TaskItemTags).HasForeignKey(x => x.TagId);
        }
    }
}
