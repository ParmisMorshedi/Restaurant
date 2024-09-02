﻿using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Data
{
    public class RestaurantContext : DbContext
    {    
        public RestaurantContext(DbContextOptions<RestaurantContext> options) :base(options){ }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Menu> Menues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Price property of the Menu entity to use a decimal type with specific precision.
            modelBuilder.Entity<Menu>()
                .Property(m => m.Price)
                .HasColumnType("decimal(18,2)");

            // Configure the Time property of the Reservation entity to store as TimeSpan in the database.
            modelBuilder.Entity<Reservation>()
               .Property(r => r.Time)
               .HasConversion(
                   v => v.ToTimeSpan(),// Convert TimeOnly to TimeSpan before saving to the database.
                   v => TimeOnly.FromTimeSpan(v)
                );

            base.OnModelCreating(modelBuilder);

        }
    }
}