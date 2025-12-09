using Microsoft.EntityFrameworkCore;

namespace DemoDangTin.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Entities.BaiDang> BaiDangs { get; set; }
        public DbSet<Entities.User> Users { get; set; }
    }
}
