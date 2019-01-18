﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using samsung.api.DataSource.Models;
using samsung_api.DataSource.Models;
using System;

namespace samsung.api.DataSource
{
    public class DatabaseContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        private readonly IConfiguration _config;

        public DatabaseContext()
        {
        }

        public DatabaseContext(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<GeneralUser> GeneralUsers { get; set; }

        public virtual DbSet<Buddy> Buddies { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<TeachingSubject> TeachingSubjects { get; set; }

        public virtual DbSet<Interest> Interests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_config != null && !optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("sqlserver"), options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
            }
            else
            {
                optionsBuilder.UseSqlServer("Data Source=samsungschoollink.cgqkqazuj2mg.eu-west-1.rds.amazonaws.com;Initial Catalog=SamsungDatabase;User Id=root;Password=MuQSFP4vVkenYnGiGOc7AunEg07LNqyt;Pooling=False;Connect Timeout=30",
                    options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // Buddy
            mb.Entity<Buddy>(entity =>
            {
                entity
                    .HasKey(key => new { key.ReceivingGeneralUserId, key.RequestingGeneralUserId });

                entity
                    .HasOne(source => source.ReceivingGeneralUser)
                    .WithMany(prop => prop.ReceivingBuddy)
                    .HasForeignKey(b => b.ReceivingGeneralUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity
                    .HasOne(source => source.RequestingGeneralUser)
                    .WithMany(prop => prop.RequestingBuddy)
                    .HasForeignKey(b => b.RequestingGeneralUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // TeachingSubject data seeds
            mb.Entity<TeachingSubject>().HasData(
                new TeachingSubject { Id = 1, Name = "Subject 1" },
                new TeachingSubject { Id = 2, Name = "Subject 2" }
            );

            mb.Entity<GeneralUserTeachingSubject>()
                .HasKey(k => new { k.GeneralUserId, k.TeachingSubjectId });

            mb.Entity<GeneralUserTeachingSubject>()
                .HasOne(g => g.GeneralUser)
                .WithMany(g => g.GeneralUserTeachingSubjects)
                .HasForeignKey(g => g.GeneralUserId);

            mb.Entity<GeneralUserTeachingSubject>()
                .HasOne(g => g.TeachingSubject)
                .WithMany(t => t.GeneralUserTeachingSubjects)
                .HasForeignKey(g => g.TeachingSubjectId);


            // Interest data seeds
            mb.Entity<Interest>().HasData(
                new Interest { Id = 1, Name = "Interest 1" },
                new Interest { Id = 2, Name = "Interest 2" }
            );

            mb.Entity<GeneralUserInterest>()
                .HasKey(k => new { k.GeneralUserId, k.InterestId });

            mb.Entity<GeneralUserInterest>()
                .HasOne(g => g.GeneralUser)
                .WithMany(g => g.GeneralUserInterests)
                .HasForeignKey(g => g.GeneralUserId);

            mb.Entity<GeneralUserInterest>()
                .HasOne(g => g.Interest)
                .WithMany(t => t.GeneralUserInterests)
                .HasForeignKey(g => g.InterestId);
        }
    }
}