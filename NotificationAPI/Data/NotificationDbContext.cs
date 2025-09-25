using Microsoft.EntityFrameworkCore;
using EmployeeService.Models;

namespace NotificationAPI.Data
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

        public DbSet<DynamicGrainRequest> Notifications { get; set; }
    }
}
