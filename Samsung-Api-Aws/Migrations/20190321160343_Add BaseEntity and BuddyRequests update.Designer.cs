﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using samsung.api.DataSource;

namespace SamsungApiAws.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190321160343_Add BaseEntity and BuddyRequests update")]
    partial class AddBaseEntityandBuddyRequestsupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("FacebookId");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<int>("LinkedInId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.FavoriteLink", b =>
                {
                    b.Property<int>("GeneralUserId");

                    b.Property<int>("LinkId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("GeneralUserId", "LinkId");

                    b.HasIndex("LinkId");

                    b.ToTable("FavoriteLinks");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Gender");

                    b.Property<Guid>("IdentityId");

                    b.Property<string>("Locale");

                    b.Property<string>("Location");

                    b.Property<int>("TechLevel");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("IdentityId");

                    b.ToTable("GeneralUsers");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserInterest", b =>
                {
                    b.Property<int>("GeneralUserId");

                    b.Property<int>("InterestId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("GeneralUserId", "InterestId");

                    b.HasIndex("InterestId");

                    b.ToTable("GeneralUserInterest");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserSeenGeneralUser", b =>
                {
                    b.Property<int>("LoggedInGeneralUserId");

                    b.Property<int?>("HasSeenGeneralUserId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("LoggedInGeneralUserId", "HasSeenGeneralUserId");

                    b.HasIndex("HasSeenGeneralUserId");

                    b.ToTable("GeneralUserSeenGeneralUser");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserSeenLink", b =>
                {
                    b.Property<int>("GeneralUserId");

                    b.Property<int>("LinkId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("GeneralUserId", "LinkId");

                    b.HasIndex("LinkId");

                    b.ToTable("GeneralUserSeenLink");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserTeachingAgeGroup", b =>
                {
                    b.Property<int>("GeneralUserId");

                    b.Property<int>("TeachingAgeGroupId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("GeneralUserId", "TeachingAgeGroupId");

                    b.HasIndex("TeachingAgeGroupId");

                    b.ToTable("GeneralUserTeachingAgeGroup");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserTeachingLevel", b =>
                {
                    b.Property<int>("GeneralUserId");

                    b.Property<int>("TeachingLevelId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("GeneralUserId", "TeachingLevelId");

                    b.HasIndex("TeachingLevelId");

                    b.ToTable("GeneralUserTeachingLevel");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserTeachingSubject", b =>
                {
                    b.Property<int>("GeneralUserId");

                    b.Property<int>("TeachingSubjectId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("GeneralUserId", "TeachingSubjectId");

                    b.HasIndex("TeachingSubjectId");

                    b.ToTable("GeneralUserTeachingSubject");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.Interest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Interests");

                    b.HasData(
                        new { Id = 1, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), Name = "Interest 1", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) },
                        new { Id = 2, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), Name = "Interest 2", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) },
                        new { Id = 3, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), Name = "Interest 3", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) },
                        new { Id = 4, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), Name = "Interest 4", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) }
                    );
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int?>("GeneralUserId");

                    b.Property<int>("ImageType");

                    b.Property<string>("ImageWebUrl");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2048);

                    b.HasKey("Id");

                    b.HasIndex("GeneralUserId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.LinkInterest", b =>
                {
                    b.Property<int>("InterestId");

                    b.Property<int>("LinkId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("InterestId", "LinkId");

                    b.HasIndex("LinkId");

                    b.ToTable("LinkInterest");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.TeachingAgeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("TeachingAgeGroups");

                    b.HasData(
                        new { Id = 1, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), Name = "10 - 15", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) },
                        new { Id = 2, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), Name = "15 - 20", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) },
                        new { Id = 3, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), Name = "25 - 30", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) },
                        new { Id = 4, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), Name = "30+", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) }
                    );
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.TeachingLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("TeachingLevels");

                    b.HasData(
                        new { Id = 1, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), Name = "VMBO", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) },
                        new { Id = 2, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), Name = "MAVO", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) },
                        new { Id = 3, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), Name = "HAVO", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) },
                        new { Id = 4, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), Name = "VWO", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) },
                        new { Id = 5, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), Name = "HBO", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) },
                        new { Id = 6, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), Name = "WO", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) },
                        new { Id = 7, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), Name = "Anders", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) }
                    );
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.TeachingSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("TeachingSubjects");

                    b.HasData(
                        new { Id = 1, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 165, DateTimeKind.Local), Name = "Subject 1", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 167, DateTimeKind.Local) },
                        new { Id = 2, CreatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 167, DateTimeKind.Local), Name = "Subject 2", UpdatedAt = new DateTime(2019, 3, 21, 17, 3, 43, 167, DateTimeKind.Local) }
                    );
                });

            modelBuilder.Entity("samsung_api.DataSource.Models.BuddyRequest", b =>
                {
                    b.Property<int?>("ReceivingGeneralUserId");

                    b.Property<int>("RequestingGeneralUserId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("RequestState");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ReceivingGeneralUserId", "RequestingGeneralUserId");

                    b.HasIndex("RequestingGeneralUserId");

                    b.ToTable("BuddyRequests");
                });

            modelBuilder.Entity("SamsungApiAws.DataSource.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityAccentName");

                    b.Property<string>("CityName");

                    b.Property<string>("CountryCode");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.FavoriteLink", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "GeneralUser")
                        .WithMany("FavoriteLinks")
                        .HasForeignKey("GeneralUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.Link", "Link")
                        .WithMany("FavoriteLinks")
                        .HasForeignKey("LinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUser", b =>
                {
                    b.HasOne("SamsungApiAws.DataSource.Models.City", "City")
                        .WithMany("GeneralUsers")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.AppUser", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserInterest", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "GeneralUser")
                        .WithMany("GeneralUserInterests")
                        .HasForeignKey("GeneralUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.Interest", "Interest")
                        .WithMany("GeneralUserInterests")
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserSeenGeneralUser", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "HasSeenGeneralUser")
                        .WithMany("HasSeenGeneralUsers")
                        .HasForeignKey("HasSeenGeneralUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "LoggedInGeneralUser")
                        .WithMany("LoggedInGeneralUsers")
                        .HasForeignKey("LoggedInGeneralUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserSeenLink", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "GeneralUser")
                        .WithMany("GeneralUserSeenLinks")
                        .HasForeignKey("GeneralUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.Link", "Link")
                        .WithMany("GeneralUserSeenLinks")
                        .HasForeignKey("LinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserTeachingAgeGroup", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "GeneralUser")
                        .WithMany("GeneralUserTeachingAgeGroups")
                        .HasForeignKey("GeneralUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.TeachingAgeGroup", "TeachingAgeGroup")
                        .WithMany("GeneralUserTeachingAgeGroups")
                        .HasForeignKey("TeachingAgeGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserTeachingLevel", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "GeneralUser")
                        .WithMany("GeneralUserTeachingLevels")
                        .HasForeignKey("GeneralUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.TeachingLevel", "TeachingLevel")
                        .WithMany("GeneralUserTeachingLevels")
                        .HasForeignKey("TeachingLevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserTeachingSubject", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "GeneralUser")
                        .WithMany("GeneralUserTeachingSubjects")
                        .HasForeignKey("GeneralUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.TeachingSubject", "TeachingSubject")
                        .WithMany("GeneralUserTeachingSubjects")
                        .HasForeignKey("TeachingSubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.Link", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "GeneralUser")
                        .WithMany("Links")
                        .HasForeignKey("GeneralUserId");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.LinkInterest", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.Interest", "Interest")
                        .WithMany("LinkInterests")
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.Link", "Link")
                        .WithMany("LinkInterests")
                        .HasForeignKey("LinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("samsung_api.DataSource.Models.BuddyRequest", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "ReceivingGeneralUser")
                        .WithMany("ReceivingBuddies")
                        .HasForeignKey("ReceivingGeneralUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "RequestingGeneralUser")
                        .WithMany("RequestingBuddies")
                        .HasForeignKey("RequestingGeneralUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
