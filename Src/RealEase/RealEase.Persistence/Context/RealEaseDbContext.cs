using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "Principal",
                    Email = "admin@realease.com",
                    PhoneNumber = "8091111111",
                    PasswordHash = "hashed-password",
                    Role = "Admin",
                    IsActive = true
                }
            );

            modelBuilder.Entity<Propertie>().HasData(
                new Propertie
                {
                    Id = 1,
                    Title = "Apartamento en Naco",
                    Image = "default.jpg",
                    Description = "Cómodo apartamento de 3 habitaciones.",
                    Address = "Naco, Santo Domingo",
                    PropertyType = "Apartamento",
                    Status = "Disponible",
                    Price = 15000000,
                    OwnerId = 1
                }
            );

            modelBuilder.Entity<Visit>().HasData(
                new Visit
                {
                    Id = 1,
                    PropertyId = 1,
                    UserId = 1,
                    Status = "Confirmada",
                    Notes = "Ejemplo",
                    VisitDate = new DateTime(2025, 4, 20, 10, 0, 0)
                }
            );

            modelBuilder.Entity<Contract>().HasData(
                new Contract
                {
                    Id = 1,
                    ClientId = 1,
                    PropertyId = 1,
                    StartDate = new DateTime(2025, 5, 1),
                    EndDate = new DateTime(2026, 5, 1),
                    Status = "Activo"
                }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    ContractId = 1,
                    Amount = 50000,
                    PaymentDate = new DateTime(2025, 5, 5),
                    PaymentMethod = "Tarjeta de Crédito",
                    Status = "Pagado"
                }
            );
        }

        }

    }

