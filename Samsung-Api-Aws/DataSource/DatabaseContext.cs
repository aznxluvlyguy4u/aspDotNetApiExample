using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using samsung.api.DataSource.Models;
using samsung_api.DataSource.Models;
using SamsungApiAws.DataSource.Models;
using System;
using System.Linq;

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

        // Lets override the default save changes, so we automaticaly change the UpdatedAt field.
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Ignore the CreatedTime updates on Modified entities. 
                    entry.Property("CreatedAt").IsModified = false;
                }
                // Always set UpdatedAt. Assuming all entities having CreatedAt property
                // Also have UpdatedAt
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_config != null && !optionsBuilder.IsConfigured)
            {
#if DEBUG
                //optionsBuilder
                //    .UseSqlServer(_config.GetConnectionString("sqlserver"), options => options.EnableRetryOnFailure());
                //base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer("Data Source=samsung-schoollink-api-db.cqzbijn95jjx.eu-west-1.rds.amazonaws.com;Initial Catalog=SamsungDatabaseStaging;User Id=root;Password=MuQSFP4vVkenYnGiGOc7AunEg07LNqyt;Pooling=False;Connect Timeout=30",
                    options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
#else
                optionsBuilder.UseSqlServer("Data Source=samsung-schoollink-api-db.cqzbijn95jjx.eu-west-1.rds.amazonaws.com;Initial Catalog=SamsungDatabase;User Id=root;Password=MuQSFP4vVkenYnGiGOc7AunEg07LNqyt;Pooling=False;Connect Timeout=30",
                    options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
#endif
            }
            else
            {
#if DEBUG
                //optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=SamsungDatabase;Integrated Security=True;Pooling=False;Connect Timeout=30",
                //    options => options.EnableRetryOnFailure());
                //base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer("Data Source=samsung-schoollink-api-db.cqzbijn95jjx.eu-west-1.rds.amazonaws.com;Initial Catalog=SamsungDatabaseStaging;User Id=root;Password=MuQSFP4vVkenYnGiGOc7AunEg07LNqyt;Pooling=False;Connect Timeout=30",
                    options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
#else
                optionsBuilder.UseSqlServer("Data Source=samsung-schoollink-api-db.cqzbijn95jjx.eu-west-1.rds.amazonaws.com;Initial Catalog=SamsungDatabase;User Id=root;Password=MuQSFP4vVkenYnGiGOc7AunEg07LNqyt;Pooling=False;Connect Timeout=30",
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
                new TeachingSubject { Id = 1, Name = "Beroepsgerichte vakken" },
                new TeachingSubject { Id = 2, Name = "Bewegingsonderwijs en sport" },
                new TeachingSubject { Id = 3, Name = "Klassieke talen" },
                new TeachingSubject { Id = 4, Name = "Nederlands" },
                new TeachingSubject { Id = 5, Name = "Rekenen & wiskunde" },
                new TeachingSubject { Id = 6, Name = "Moderne vreemde talen" },
                new TeachingSubject { Id = 7, Name = "Kunst & cultuur" },
                new TeachingSubject { Id = 8, Name = "Mens & maatschappij" },
                new TeachingSubject { Id = 9, Name = "Natuur & techniek" },
                new TeachingSubject { Id = 10, Name = "Anders" }
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
                new TeachingLevel { Id = 1, Name = "Speciaal onderwijs" },
                new TeachingLevel { Id = 2, Name = "Basisonderwijs" },
                new TeachingLevel { Id = 3, Name = "VMBO" },
                new TeachingLevel { Id = 4, Name = "Havo" },
                new TeachingLevel { Id = 5, Name = "Vwo/Gymnasium" },
                new TeachingLevel { Id = 6, Name = "MBO" },
                new TeachingLevel { Id = 7, Name = "WO" },
                new TeachingLevel { Id = 8, Name = "ASO" },
                new TeachingLevel { Id = 9, Name = "TSO" },
                new TeachingLevel { Id = 10, Name = "BSO" },
                new TeachingLevel { Id = 11, Name = "KSO" },
                new TeachingLevel { Id = 12, Name = "Lerarenopleiding" },
                new TeachingLevel { Id = 13, Name = "Volwassenenonderwijs" }
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
                new Interest { Id = 1, Name = "Algoritmes" },
                new Interest { Id = 2, Name = "Animaties maken" },
                new Interest { Id = 3, Name = "Annotatietools" },
                new Interest { Id = 4, Name = "Artificial intelligence" },
                new Interest { Id = 5, Name = "Apps" },
                new Interest { Id = 6, Name = "Augmented Reality" },
                new Interest { Id = 7, Name = "Bloggen" },
                new Interest { Id = 8, Name = "Connected Toys" },
                new Interest { Id = 9, Name = "Content Cureren" },
                new Interest { Id = 10, Name = "Cybercrime" },
                new Interest { Id = 11, Name = "Cyberpesten" },
                new Interest { Id = 12, Name = "Discussies" },
                new Interest { Id = 13, Name = "Elektronisch portfolio" },
                new Interest { Id = 14, Name = "eSports" },
                new Interest { Id = 15, Name = "Filterbubbels" },
                new Interest { Id = 16, Name = "Gamen" },
                new Interest { Id = 17, Name = "Gamestreaming" },
                new Interest { Id = 18, Name = "Gamificationtools" },
                new Interest { Id = 19, Name = "Grooming" },
                new Interest { Id = 20, Name = "Infographics maken" },
                new Interest { Id = 21, Name = "Internet of Things" },
                new Interest { Id = 22, Name = "Kunstmatige intelligentie" },
                new Interest { Id = 23, Name = "Lerende vragen laten maken" },
                new Interest { Id = 24, Name = "Lesmateriaal" },
                new Interest { Id = 25, Name = "Livestreamen" },
                new Interest { Id = 26, Name = "Mediaopvoeding" },
                new Interest { Id = 27, Name = "Mediagebruik" },
                new Interest { Id = 28, Name = "Mediawijsheid" },
                new Interest { Id = 29, Name = "Naakfoto’s" },
                new Interest { Id = 30, Name = "Online pesten" },
                new Interest { Id = 31, Name = "Podcasting" },
                new Interest { Id = 32, Name = "Podcasts" },
                new Interest { Id = 33, Name = "Posters maken" },
                new Interest { Id = 34, Name = "Presentaties" },
                new Interest { Id = 35, Name = "Robots" },
                new Interest { Id = 36, Name = "Samen in documenten werken" },
                new Interest { Id = 37, Name = "Sextortion" },
                new Interest { Id = 38, Name = "Serious Games" },
                new Interest { Id = 39, Name = "Sexting" },
                new Interest { Id = 40, Name = "Storytelling" },
                new Interest { Id = 41, Name = "Student response systeem" },
                new Interest { Id = 42, Name = "Veiligheid" },
                new Interest { Id = 43, Name = "Video’s gebruiken" },
                new Interest { Id = 44, Name = "Virtual Reality" },
                new Interest { Id = 45, Name = "Vloggen" },
                new Interest { Id = 46, Name = "Webinars" },
                new Interest { Id = 47, Name = "Weblectures" },
                new Interest { Id = 48, Name = "Wraakporno" }
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
                new TeachingAgeGroup { Id = 1, Name = "4 - 6" },
                new TeachingAgeGroup { Id = 2, Name = "6 - 8" },
                new TeachingAgeGroup { Id = 3, Name = "6 - 9" },
                new TeachingAgeGroup { Id = 4, Name = "8 - 10" },
                new TeachingAgeGroup { Id = 5, Name = "9 - 12" },
                new TeachingAgeGroup { Id = 6, Name = "10 - 12" },
                new TeachingAgeGroup { Id = 7, Name = "12 - 14" },
                new TeachingAgeGroup { Id = 8, Name = "12 - 15" },
                new TeachingAgeGroup { Id = 9, Name = "14 - 16" },
                new TeachingAgeGroup { Id = 10, Name = "16 - 18" },
                new TeachingAgeGroup { Id = 11, Name = "15 - 20" },
                new TeachingAgeGroup { Id = 12, Name = "18+" },
                new TeachingAgeGroup { Id = 13, Name = "20+" }
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