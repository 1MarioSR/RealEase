﻿using Microsoft.EntityFrameworkCore;
using RealEase.Domain.Entities;

namespace RealEase.Domain
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

    }

}