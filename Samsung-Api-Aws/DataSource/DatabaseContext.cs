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

        public virtual DbSet<BuddyRequest> BuddyRequests { get; set; }

        public virtual DbSet<GeneralUserSeenGeneralUser> GeneralUserSeenGeneralUser { get; set; }

        public virtual DbSet<GeneralUserSeenLink> GeneralUserSeenLink { get; set; }

        public virtual DbSet<Link> Links { get; set; }

        public virtual DbSet<FavoriteLink> FavoriteLinks { get; set; }

        public virtual DbSet<TeachingSubject> TeachingSubjects { get; set; }

        public virtual DbSet<TeachingLevel> TeachingLevels { get; set; }

        public virtual DbSet<Interest> Interests { get; set; }

        public virtual DbSet<TeachingAgeGroup> TeachingAgeGroups { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_config != null && !optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("sqlserver"), options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
#else
                optionsBuilder.UseSqlServer("Data Source=samsung-ii.cgqkqazuj2mg.eu-west-1.rds.amazonaws.com;Initial Catalog=SamsungDatabase;User Id=root;Password=MuQSFP4vVkenYnGiGOc7AunEg07LNqyt;Pooling=False;Connect Timeout=30",
                    options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
#endif
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

            // Buddy
            mb.Entity<BuddyRequest>(entity =>
            {
                entity
                    .HasKey(key => new { key.ReceivingGeneralUserId, key.RequestingGeneralUserId });
                entity
                    .HasOne(source => source.ReceivingGeneralUser)
                    .WithMany(prop => prop.ReceivingBuddies)
                    .HasForeignKey(b => b.ReceivingGeneralUserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity
                    .HasOne(source => source.RequestingGeneralUser)
                    .WithMany(prop => prop.RequestingBuddies)
                    .HasForeignKey(b => b.RequestingGeneralUserId)
                    .OnDelete(DeleteBehavior.Restrict);
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
                new Interest { Id = 2, Name = "Interest 2" },
                new Interest { Id = 3, Name = "Interest 3" },
                new Interest { Id = 4, Name = "Interest 4" }
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
            mb.Entity<TeachingAgeGroup>().HasData(
                new TeachingAgeGroup { Id = 1, Name = "10 - 15" },
                new TeachingAgeGroup { Id = 2, Name = "15 - 20" },
                new TeachingAgeGroup { Id = 3, Name = "25 - 30" },
                new TeachingAgeGroup { Id = 4, Name = "30+" }
            );

            mb.Entity<GeneralUserTeachingAgeGroup>()
                .HasKey(k => new { k.GeneralUserId, k.TeachingAgeGroupId });

            mb.Entity<GeneralUserTeachingAgeGroup>()
                .HasOne(g => g.GeneralUser)
                .WithMany(g => g.GeneralUserTeachingAgeGroups)
                .HasForeignKey(g => g.GeneralUserId);

            mb.Entity<GeneralUserTeachingAgeGroup>()
                .HasOne(g => g.TeachingAgeGroup)
                .WithMany(t => t.GeneralUserTeachingAgeGroups)
                .HasForeignKey(g => g.TeachingAgeGroupId);

            // Configure FavoriteLink
            mb.Entity<FavoriteLink>()
                .HasKey(k => new { k.GeneralUserId, k.LinkId });

            mb.Entity<FavoriteLink>()
                .HasOne(g => g.GeneralUser)
                .WithMany(g => g.FavoriteLinks)
                .HasForeignKey(g => g.GeneralUserId);

            mb.Entity<FavoriteLink>()
                .HasOne(g => g.Link)
                .WithMany(g => g.FavoriteLinks)
                .HasForeignKey(g => g.LinkId);

            // Configure LinkInterest
            mb.Entity<LinkInterest>()
                .HasKey(k => new { k.InterestId, k.LinkId });

            mb.Entity<LinkInterest>()
                .HasOne(g => g.Interest)
                .WithMany(g => g.LinkInterests)
                .HasForeignKey(g => g.InterestId);

            mb.Entity<LinkInterest>()
                .HasOne(g => g.Link)
                .WithMany(g => g.LinkInterests)
                .HasForeignKey(g => g.LinkId);

            // Configure seen generaluser
            mb.Entity<GeneralUserSeenGeneralUser>()
                .HasKey(k => new { k.LoggedInGeneralUserId, k.HasSeenGeneralUserId });

            mb.Entity<GeneralUserSeenGeneralUser>()
                .HasOne(g => g.LoggedInGeneralUser)
                .WithMany(g => g.LoggedInGeneralUsers)
                .HasForeignKey(g => g.LoggedInGeneralUserId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<GeneralUserSeenGeneralUser>()
                .HasOne(g => g.HasSeenGeneralUser)
                .WithMany(t => t.HasSeenGeneralUsers)
                .HasForeignKey(g => g.HasSeenGeneralUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure seen link
            mb.Entity<GeneralUserSeenLink>()
               .HasKey(k => new { k.GeneralUserId, k.LinkId });

            mb.Entity<GeneralUserSeenLink>()
                .HasOne(g => g.GeneralUser)
                .WithMany(g => g.GeneralUserSeenLinks)
                .HasForeignKey(g => g.GeneralUserId);

            mb.Entity<GeneralUserSeenLink>()
                .HasOne(g => g.Link)
                .WithMany(g => g.GeneralUserSeenLinks)
                .HasForeignKey(g => g.LinkId);
        }
    }
}