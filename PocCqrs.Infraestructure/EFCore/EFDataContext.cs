namespace PocCqrs.Infrastructure.EFCore
{
    using Microsoft.EntityFrameworkCore;
    using PocCqrs.Domain;
    using PocCqrs.Domain.Entities;
    using System;
    using System.Reflection;
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                 .HasKey("Id");
            modelBuilder.Entity<Product>().Property("Code");
            modelBuilder.Entity<Product>().Property("Name");
            modelBuilder.Entity<Product>().Property("Description");
            modelBuilder.Entity<Product>().Property("CreatedAt");
            modelBuilder.Entity<Product>().Property("ModifiedAt");
        }
        public DbSet<Product> Products { get; set; }




    }
}
