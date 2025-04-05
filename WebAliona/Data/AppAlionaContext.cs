using FirstSp.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebAliona.Data
{
    public class AppAlionaContext : DbContext
    {
        public AppAlionaContext(DbContextOptions<AppAlionaContext> options)
            : base(options) { }
        

        public DbSet<Banan> Banans { get; set; }
        
        public DbSet<New> News { get; set; }
    }
}
