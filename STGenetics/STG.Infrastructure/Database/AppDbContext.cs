using Microsoft.EntityFrameworkCore;
using STG.Domain.Entity;

namespace STG.Infrastructure.Databse
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; }

    }
}
