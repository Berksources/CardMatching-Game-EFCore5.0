﻿// <auto-generated />
using System;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityLayer.Migrations
{
    [DbContext(typeof(KartOyunuDBContext))]
    partial class KartOyunuDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityLayer.GameCart.Cart", b =>
                {
                    b.Property<int>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CartName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DataGuidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CartID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("EntityLayer.GameCart.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CategoryIsActive")
                        .HasColumnType("bit");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DataGuidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EntityLayer.GameCart.FileRepo", b =>
                {
                    b.Property<int>("FileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DataGuidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FilePhotoIsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FileID");

                    b.HasIndex("CartID");

                    b.ToTable("FirstFileRepos");
                });

            modelBuilder.Entity("EntityLayer.Result.GameVariant", b =>
                {
                    b.Property<int>("GameVariantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DataGuidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("GameIsCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("GameIsOver")
                        .HasColumnType("bit");

                    b.Property<int>("GameQuestionCount")
                        .HasColumnType("int");

                    b.Property<int>("GameScoreCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GameVariantID");

                    b.ToTable("GameVariants");

                    b.HasData(
                        new
                        {
                            GameVariantID = 1,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataGuidID = new Guid("00a7a387-fcf3-4e9c-943f-92964054347e"),
                            GameIsCompleted = false,
                            GameIsOver = false,
                            GameQuestionCount = 0,
                            GameScoreCount = 0
                        });
                });

            modelBuilder.Entity("EntityLayer.Result.ScoreTable", b =>
                {
                    b.Property<int>("ScoreTableID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DataGuidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScoreData")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ScoreTableID");

                    b.ToTable("ScoreTables");

                    b.HasData(
                        new
                        {
                            ScoreTableID = 1,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataGuidID = new Guid("b928a487-8342-4f31-99f6-61c3e48cf595"),
                            ScoreData = 7600,
                            UserID = 2
                        });
                });

            modelBuilder.Entity("EntityLayer.UserConfig.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DataGuidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEMail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("UserIsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("UserIsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("UserLastScore")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRoleID")
                        .HasColumnType("int");

                    b.Property<string>("UserSurname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("UserRoleID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            CreatedDate = new DateTime(2021, 5, 13, 13, 56, 25, 264, DateTimeKind.Local).AddTicks(5447),
                            DataGuidID = new Guid("8ab5fb2b-4de0-476b-ae97-6d00462295f1"),
                            UserEMail = "AdminPaneli@gmail.com",
                            UserIsActive = true,
                            UserIsEmailConfirmed = true,
                            UserName = "Admin",
                            UserPassword = "AdminGitHub",
                            UserRoleID = 1,
                            UserSurname = "Admin"
                        },
                        new
                        {
                            UserID = 2,
                            CreatedDate = new DateTime(2021, 5, 13, 13, 56, 25, 266, DateTimeKind.Local).AddTicks(8284),
                            DataGuidID = new Guid("efac2860-bba9-4554-99be-7731f1431226"),
                            UserEMail = "Kullanici@gmail.com",
                            UserIsActive = true,
                            UserIsEmailConfirmed = true,
                            UserName = "Kullanici",
                            UserPassword = "KullaniciGitHub",
                            UserRoleID = 2,
                            UserSurname = "Kullanici"
                        });
                });

            modelBuilder.Entity("EntityLayer.UserConfig.UserRole", b =>
                {
                    b.Property<int>("UserRoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DataGuidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserRoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserRoleID");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserRoleID = 1,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataGuidID = new Guid("1714301c-729e-4607-9054-275b29059a1f"),
                            UserRoleName = "Admin"
                        },
                        new
                        {
                            UserRoleID = 2,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataGuidID = new Guid("89f3966c-d9df-469b-ab52-076e2b282b5d"),
                            UserRoleName = "Oyuncu"
                        });
                });

            modelBuilder.Entity("EntityLayer.GameCart.Cart", b =>
                {
                    b.HasOne("EntityLayer.GameCart.Category", "Category")
                        .WithMany("Carts")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EntityLayer.GameCart.FileRepo", b =>
                {
                    b.HasOne("EntityLayer.GameCart.Cart", "Cart")
                        .WithMany("FirstFileRepos")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("EntityLayer.Result.ScoreTable", b =>
                {
                    b.HasOne("EntityLayer.UserConfig.User", "User")
                        .WithMany("ScoreTables")
                        .HasForeignKey("ScoreTableID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("User");
                });

            modelBuilder.Entity("EntityLayer.UserConfig.User", b =>
                {
                    b.HasOne("EntityLayer.UserConfig.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("EntityLayer.GameCart.Cart", b =>
                {
                    b.Navigation("FirstFileRepos");
                });

            modelBuilder.Entity("EntityLayer.GameCart.Category", b =>
                {
                    b.Navigation("Carts");
                });

            modelBuilder.Entity("EntityLayer.UserConfig.User", b =>
                {
                    b.Navigation("ScoreTables");
                });

            modelBuilder.Entity("EntityLayer.UserConfig.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}