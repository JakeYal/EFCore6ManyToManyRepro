using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Project
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(x => x.Permissions)
                //We don't specify a navigation property here - this works fine in EF Core 5.0.8
                .WithMany("Roles")
                .UsingEntity<RolePermission>(
                    x => x.HasOne(x => x.Permission).WithMany(),
                    x => x.HasOne(x => x.Role).WithMany());
        }
    }
}
