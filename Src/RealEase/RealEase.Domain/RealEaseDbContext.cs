
using Microsoft.EntityFrameworkCore;
using RealEase.Domain.Entities;

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