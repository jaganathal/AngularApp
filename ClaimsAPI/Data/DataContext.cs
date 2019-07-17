using ClaimsAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClaimsAPI.Data
{
    public class DataContext : DbContext
    {
       public DataContext(DbContextOptions<DataContext> options):base(options){}
        public DbSet<CrawfordClaim> Claims { get; set; }
        public DbSet<User> Users { get; set; }
    }
}