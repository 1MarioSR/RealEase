using Microsoft.EntityFrameworkCore;
using RealEase.Domain.Entities;

namespace RealEase.Persistence.Context
{
    public class RealEaseDbContext : DbContext
    {
        public RealEaseDbContext()
        {
        }

        public RealEaseDbContext(DbContextOptions<RealEaseDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Propertie> Properties { get; set; }

        public DbSet<Visit> Visits { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Payment> Payments { get; set; }

    }
}

      

