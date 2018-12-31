﻿//// <auto-generated />
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage;
//using Microsoft.EntityFrameworkCore.Storage.Internal;
//using SRUK.Data;
//using System;

//namespace SRUK.Data.Migrations
//{
//    [DbContext(typeof(ApplicationDbContext))]
//    [Migration("20181204121358_BringingBackIdentitySchemaMigration")]
//    partial class BringingBackIdentitySchemaMigration
//    {
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
//                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
//                {
//                    b.Property<string>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<string>("ConcurrencyStamp")
//                        .IsConcurrencyToken();

//                    b.Property<string>("Name")
//                        .HasMaxLength(256);

//                    b.Property<string>("NormalizedName")
//                        .HasMaxLength(256);

//                    b.HasKey("Id");

//                    b.HasIndex("NormalizedName")
//                        .IsUnique()
//                        .HasName("RoleNameIndex")
//                        .HasFilter("[NormalizedName] IS NOT NULL");

//                    b.ToTable("AspNetRoles");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<string>("ClaimType");

//                    b.Property<string>("ClaimValue");

//                    b.Property<string>("RoleId")
//                        .IsRequired();

//                    b.HasKey("Id");

//                    b.HasIndex("RoleId");

//                    b.ToTable("AspNetRoleClaims");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<string>("ClaimType");

//                    b.Property<string>("ClaimValue");

//                    b.Property<string>("UserId")
//                        .IsRequired();

//                    b.HasKey("Id");

//                    b.HasIndex("UserId");

//                    b.ToTable("AspNetUserClaims");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
//                {
//                    b.Property<string>("LoginProvider");

//                    b.Property<string>("ProviderKey");

//                    b.Property<string>("ProviderDisplayName");

//                    b.Property<string>("UserId")
//                        .IsRequired();

//                    b.HasKey("LoginProvider", "ProviderKey");

//                    b.HasIndex("UserId");

//                    b.ToTable("AspNetUserLogins");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
//                {
//                    b.Property<string>("UserId");

//                    b.Property<string>("RoleId");

//                    b.HasKey("UserId", "RoleId");

//                    b.HasIndex("RoleId");

//                    b.ToTable("AspNetUserRoles");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
//                {
//                    b.Property<string>("UserId");

//                    b.Property<string>("LoginProvider");

//                    b.Property<string>("Name");

//                    b.Property<string>("Value");

//                    b.HasKey("UserId", "LoginProvider", "Name");

//                    b.ToTable("AspNetUserTokens");
//                });

//            modelBuilder.Entity("SRUK.Entities.ApplicationUser", b =>
//                {
//                    b.Property<string>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<int>("AccessFailedCount");

//                    b.Property<string>("Address");

//                    b.Property<string>("City");

//                    b.Property<string>("ConcurrencyStamp")
//                        .IsConcurrencyToken();

//                    b.Property<string>("Country");

//                    b.Property<DateTime>("CreationDate")
//                        .ValueGeneratedOnAdd()
//                        .HasDefaultValueSql("getdate()");

//                    b.Property<string>("Degree");

//                    b.Property<DateTime>("EditDate");

//                    b.Property<string>("Email")
//                        .HasMaxLength(256);

//                    b.Property<bool>("EmailConfirmed");

//                    b.Property<string>("FirstName")
//                        .HasMaxLength(50);

//                    b.Property<bool>("IsDeleted");

//                    b.Property<string>("LastName")
//                        .HasMaxLength(50);

//                    b.Property<bool>("LockoutEnabled");

//                    b.Property<DateTimeOffset?>("LockoutEnd");

//                    b.Property<string>("NormalizedEmail")
//                        .HasMaxLength(256);

//                    b.Property<string>("NormalizedUserName")
//                        .HasMaxLength(256);

//                    b.Property<string>("Organisation")
//                        .HasMaxLength(100);

//                    b.Property<string>("OrganisationAdderss");

//                    b.Property<string>("PasswordHash");

//                    b.Property<string>("PhoneNumber");

//                    b.Property<bool>("PhoneNumberConfirmed");

//                    b.Property<string>("PostalCode");

//                    b.Property<string>("SecurityStamp");

//                    b.Property<bool>("TwoFactorEnabled");

//                    b.Property<string>("UserName")
//                        .HasMaxLength(256);

//                    b.Property<string>("VATID");

//                    b.HasKey("Id");

//                    b.HasIndex("NormalizedEmail")
//                        .HasName("EmailIndex");

//                    b.HasIndex("NormalizedUserName")
//                        .IsUnique()
//                        .HasName("UserNameIndex")
//                        .HasFilter("[NormalizedUserName] IS NOT NULL");

//                    b.ToTable("AspNetUsers");
//                });

//            modelBuilder.Entity("SRUK.Entities.Paper", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<DateTime>("CreationDate")
//                        .ValueGeneratedOnAdd()
//                        .HasDefaultValueSql("getdate()");

//                    b.Property<DateTime>("EditDate");

//                    b.Property<bool>("IsDeleted");

//                    b.Property<long>("ParticipancyId");

//                    b.Property<DateTime>("SentToPrintDate");

//                    b.Property<byte>("Status");

//                    b.Property<string>("Title")
//                        .IsRequired()
//                        .HasMaxLength(200);

//                    b.HasKey("Id");

//                    b.HasIndex("ParticipancyId");

//                    b.ToTable("Paper");
//                });

//            modelBuilder.Entity("SRUK.Entities.PaperVersion", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<string>("Comment");

//                    b.Property<DateTime>("CreationDate")
//                        .ValueGeneratedOnAdd()
//                        .HasDefaultValueSql("getdate()");

//                    b.Property<DateTime>("EditDate");

//                    b.Property<string>("FileName")
//                        .IsRequired();

//                    b.Property<bool>("IsDeleted");

//                    b.Property<string>("OriginalFileName")
//                        .IsRequired();

//                    b.Property<long>("PaperId");

//                    b.Property<byte>("Status");

//                    b.HasKey("Id");

//                    b.HasIndex("PaperId");

//                    b.ToTable("PaperVerison");
//                });

//            modelBuilder.Entity("SRUK.Entities.Participancy", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<bool>("ConferenceParticipation");

//                    b.Property<DateTime>("CreationDate")
//                        .ValueGeneratedOnAdd()
//                        .HasDefaultValueSql("getdate()");

//                    b.Property<DateTime>("EditDate");

//                    b.Property<bool>("IsDeleted");

//                    b.Property<bool>("Publication");

//                    b.Property<long>("SeasonId");

//                    b.Property<string>("UserId")
//                        .IsRequired();

//                    b.HasKey("Id");

//                    b.HasIndex("SeasonId");

//                    b.HasIndex("UserId");

//                    b.ToTable("Participancy");
//                });

//            modelBuilder.Entity("SRUK.Entities.Review", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<string>("Comment");

//                    b.Property<DateTime>("CompletionDate");

//                    b.Property<DateTime>("CreationDate")
//                        .ValueGeneratedOnAdd()
//                        .HasDefaultValueSql("getdate()");

//                    b.Property<string>("CriticId")
//                        .IsRequired();

//                    b.Property<DateTime>("Deadline");

//                    b.Property<DateTime>("EditDate");

//                    b.Property<bool?>("EditorialErrors");

//                    b.Property<string>("FileName");

//                    b.Property<bool>("IsDeleted");

//                    b.Property<bool?>("IsPositive");

//                    b.Property<string>("OriginalFileName");

//                    b.Property<long>("PaperVersionId");

//                    b.Property<bool?>("RepeatReview");

//                    b.Property<byte>("Status");

//                    b.Property<bool?>("TechnicalErrors");

//                    b.Property<bool?>("Unsuitable");

//                    b.HasKey("Id");

//                    b.HasIndex("CriticId");

//                    b.HasIndex("PaperVersionId");

//                    b.ToTable("Review");
//                });

//            modelBuilder.Entity("SRUK.Entities.Season", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<DateTime>("CreationDate")
//                        .ValueGeneratedOnAdd()
//                        .HasDefaultValueSql("getdate()");

//                    b.Property<DateTime>("EditDate");

//                    b.Property<DateTime>("EndDate");

//                    b.Property<bool>("IsDeleted");

//                    b.Property<string>("LogoFileName")
//                        .IsRequired();

//                    b.Property<string>("MainImageFileName")
//                        .IsRequired();

//                    b.Property<string>("Name")
//                        .IsRequired();

//                    b.Property<DateTime>("StartDate");

//                    b.HasKey("Id");

//                    b.ToTable("Season");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
//                {
//                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
//                        .WithMany()
//                        .HasForeignKey("RoleId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
//                {
//                    b.HasOne("SRUK.Entities.ApplicationUser")
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
//                {
//                    b.HasOne("SRUK.Entities.ApplicationUser")
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
//                {
//                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
//                        .WithMany()
//                        .HasForeignKey("RoleId")
//                        .OnDelete(DeleteBehavior.Cascade);

//                    b.HasOne("SRUK.Entities.ApplicationUser")
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
//                {
//                    b.HasOne("SRUK.Entities.ApplicationUser")
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });

//            modelBuilder.Entity("SRUK.Entities.Paper", b =>
//                {
//                    b.HasOne("SRUK.Entities.Participancy", "Participancy")
//                        .WithMany("Papers")
//                        .HasForeignKey("ParticipancyId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });

//            modelBuilder.Entity("SRUK.Entities.PaperVersion", b =>
//                {
//                    b.HasOne("SRUK.Entities.Paper", "Paper")
//                        .WithMany("PaperVersions")
//                        .HasForeignKey("PaperId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });

//            modelBuilder.Entity("SRUK.Entities.Participancy", b =>
//                {
//                    b.HasOne("SRUK.Entities.Season", "Season")
//                        .WithMany("Participancies")
//                        .HasForeignKey("SeasonId")
//                        .OnDelete(DeleteBehavior.Cascade);

//                    b.HasOne("SRUK.Entities.ApplicationUser", "User")
//                        .WithMany("Participancies")
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });

//            modelBuilder.Entity("SRUK.Entities.Review", b =>
//                {
//                    b.HasOne("SRUK.Entities.ApplicationUser", "Critic")
//                        .WithMany("Reviews")
//                        .HasForeignKey("CriticId")
//                        .OnDelete(DeleteBehavior.Cascade);

//                    b.HasOne("SRUK.Entities.PaperVersion", "PaperVersion")
//                        .WithMany("Reviews")
//                        .HasForeignKey("PaperVersionId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
