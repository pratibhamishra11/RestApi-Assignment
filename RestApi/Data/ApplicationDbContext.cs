using Microsoft.EntityFrameworkCore;
using RestApi.Models;
namespace RestApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            { }

        public DbSet<Beverages> BeveragesL { get; set; } = null!;
    }
}
