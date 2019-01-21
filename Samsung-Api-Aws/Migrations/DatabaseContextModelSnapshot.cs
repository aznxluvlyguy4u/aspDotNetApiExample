﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using samsung.api.DataSource;

namespace samsung.api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
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

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("FacebookId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

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

                    b.Property<int>("TechLevel");

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

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Gender");

                    b.Property<Guid>("IdentityId");

                    b.Property<string>("Locale");

                    b.Property<string>("Location");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId");

                    b.ToTable("GeneralUsers");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserInterest", b =>
                {
                    b.Property<int>("GeneralUserId");

                    b.Property<int>("InterestId");

                    b.HasKey("GeneralUserId", "InterestId");

                    b.HasIndex("InterestId");

                    b.ToTable("GeneralUserInterest");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserTeachingLevel", b =>
                {
                    b.Property<int>("GeneralUserId");

                    b.Property<int>("TeachingLevelId");

                    b.HasKey("GeneralUserId", "TeachingLevelId");

                    b.HasIndex("TeachingLevelId");

                    b.ToTable("GeneralUserTeachingLevel");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUserTeachingSubject", b =>
                {
                    b.Property<int>("GeneralUserId");

                    b.Property<int>("TeachingSubjectId");

                    b.HasKey("GeneralUserId", "TeachingSubjectId");

                    b.HasIndex("TeachingSubjectId");

                    b.ToTable("GeneralUserTeachingSubject");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.Interest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Interests");

                    b.HasData(
                        new { Id = 1, Name = "Interest 1" },
                        new { Id = 2, Name = "Interest 2" }
                    );
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.TeachingLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TeachingLevels");

                    b.HasData(
                        new { Id = 1, Name = "VMBO" },
                        new { Id = 2, Name = "MAVO" },
                        new { Id = 3, Name = "HAVO" },
                        new { Id = 4, Name = "VWO" },
                        new { Id = 5, Name = "HBO" },
                        new { Id = 6, Name = "WO" },
                        new { Id = 7, Name = "Anders" }
                    );
                });

            modelBuilder.Entity("samsung.api.DataSource.Models.TeachingSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TeachingSubjects");

                    b.HasData(
                        new { Id = 1, Name = "Subject 1" },
                        new { Id = 2, Name = "Subject 2" }
                    );
                });

            modelBuilder.Entity("samsung_api.DataSource.Models.Buddy", b =>
                {
                    b.Property<int>("ReceivingGeneralUserId");

                    b.Property<int>("RequestingGeneralUserId");

                    b.Property<int>("RequestState");

                    b.HasKey("ReceivingGeneralUserId", "RequestingGeneralUserId");

                    b.HasIndex("RequestingGeneralUserId");

                    b.ToTable("Buddies");
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

            modelBuilder.Entity("samsung.api.DataSource.Models.GeneralUser", b =>
                {
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

            modelBuilder.Entity("samsung_api.DataSource.Models.Buddy", b =>
                {
                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "ReceivingGeneralUser")
                        .WithMany("ReceivingBuddy")
                        .HasForeignKey("ReceivingGeneralUserId");

                    b.HasOne("samsung.api.DataSource.Models.GeneralUser", "RequestingGeneralUser")
                        .WithMany("RequestingBuddy")
                        .HasForeignKey("RequestingGeneralUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
