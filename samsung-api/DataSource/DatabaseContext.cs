﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using samsung.api.DataSource.Models;
using samsung_api.DataSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.DataSource
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _config;

        public DatabaseContext()
        {
        }

        public DatabaseContext(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<Buddies> Buddies { get; set; }

        public virtual DbSet<Profile> Profiles { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_config != null && !optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("sqlserver"), options => options.EnableRetryOnFailure());
            }
            else
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=SamsungDatabase;Integrated Security=True;Pooling=False;Connect Timeout=30", 
                    options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Buddies>(entity =>
            {
                entity
                    .HasKey(key => new { key.ReceivingProfileId, key.RequestingProfileId});

                entity
                    .HasOne(source => source.ReceivingProfile)
                    .WithMany(prop => prop.ReceivingBuddy)
                    .HasForeignKey(b => b.ReceivingProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity
                    .HasOne(source => source.RequestingProfile)
                    .WithMany(prop=>prop.RequestingBuddy)
                    .HasForeignKey(b => b.RequestingProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
