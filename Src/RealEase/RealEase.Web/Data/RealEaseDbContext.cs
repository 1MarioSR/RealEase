using RealEase.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace RealEase.Web.Data
{
    public class RealEaseDbContext : DbContext
    {
        public RealEaseDbContext()
        {
        }

        public RealEaseDbContext(DbContextOptions<RealEaseDbContext> options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }

        
    }

}