using ClaimsAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClaimsAPI.Data
{
    public class DataContext : DbContext
    {
       public DataContext(DbContextOptions<DataContext> options):base(options){}
        public DbSet<Claim> Claims { get; set; }
    }
}