using Microsoft.EntityFrameworkCore;

namespace HelloWorldApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ToDo> ToDo => Set<ToDo>();

    }
}
