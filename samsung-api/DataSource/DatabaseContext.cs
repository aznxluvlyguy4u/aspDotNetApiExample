using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using samsung.api.DataSource.Models;
using samsung_api.DataSource.Models;

namespace samsung.api.DataSource
{
    public class DatabaseContext
        : IdentityDbContext<AppUser>
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

        public virtual DbSet<Buddies> Buddies { get; set; }

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
            base.OnModelCreating(mb);

            mb.Entity<AppUser>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            mb.Entity<Buddies>(entity =>
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
        }
    }
}