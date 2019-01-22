using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using samsung.api.DataSource.Models;
using samsung_api.DataSource.Models;
using SamsungApiAws.DataSource.Models;
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

        public virtual DbSet<TeachingLevel> TeachingLevels { get; set; }

        public virtual DbSet<Interest> Interests { get; set; }

        public virtual DbSet<AgeGroup> AgeGroups { get; set; }

        public virtual DbSet<City> Cities { get; set; }

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
#if DEBUG
                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=SamsungDatabase;Integrated Security=True;Pooling=False;Connect Timeout=30",
                    options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
#else
                optionsBuilder.UseSqlServer("Data Source=samsung-ii.cgqkqazuj2mg.eu-west-1.rds.amazonaws.com;Initial Catalog=SamsungDatabase;User Id=root;Password=MuQSFP4vVkenYnGiGOc7AunEg07LNqyt;Pooling=False;Connect Timeout=30",
                    options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // GeneralUser
            mb.Entity<GeneralUser>()
                .HasOne(g => g.AgeGroup)
                .WithMany(a => a.GeneralUsers);

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

            // TeachingLevel data seeds
            mb.Entity<TeachingLevel>().HasData(
                new TeachingLevel { Id = 1, Name = "VMBO" },
                new TeachingLevel { Id = 2, Name = "MAVO" },
                new TeachingLevel { Id = 3, Name = "HAVO" },
                new TeachingLevel { Id = 4, Name = "VWO" },
                new TeachingLevel { Id = 5, Name = "HBO" },
                new TeachingLevel { Id = 6, Name = "WO" },
                new TeachingLevel { Id = 7, Name = "Anders" }
            );

            mb.Entity<GeneralUserTeachingLevel>()
                .HasKey(k => new { k.GeneralUserId, k.TeachingLevelId });

            mb.Entity<GeneralUserTeachingLevel>()
                .HasOne(g => g.GeneralUser)
                .WithMany(g => g.GeneralUserTeachingLevels)
                .HasForeignKey(g => g.GeneralUserId);

            mb.Entity<GeneralUserTeachingLevel>()
                .HasOne(g => g.TeachingLevel)
                .WithMany(t => t.GeneralUserTeachingLevels)
                .HasForeignKey(g => g.TeachingLevelId);

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

            // AgeGroup data seeds
            mb.Entity<AgeGroup>().HasData(
                new AgeGroup { Id = 1, Name = "10 - 15" },
                new AgeGroup { Id = 2, Name = "15 - 20" },
                new AgeGroup { Id = 3, Name = "25 - 30" },
                new AgeGroup { Id = 4, Name = "30+" }
            );
        }
    }
}