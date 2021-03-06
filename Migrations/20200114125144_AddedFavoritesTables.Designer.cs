﻿// <auto-generated />
using System;
using AuthExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AuthExample.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200114125144_AddedFavoritesTables")]
    partial class AddedFavoritesTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AuthExample.Models.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("UserFavoriteId")
                        .HasColumnType("integer");

                    b.Property<int>("fdcId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserFavoriteId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("AuthExample.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileUrl")
                        .HasColumnType("text");

                    b.Property<int?>("UserFavoriteId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserFavoriteId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AuthExample.Models.UserFavorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FavoriteId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserFavorites");
                });

            modelBuilder.Entity("AuthExample.Models.Favorite", b =>
                {
                    b.HasOne("AuthExample.Models.UserFavorite", null)
                        .WithMany("Favorites")
                        .HasForeignKey("UserFavoriteId");
                });

            modelBuilder.Entity("AuthExample.Models.User", b =>
                {
                    b.HasOne("AuthExample.Models.UserFavorite", null)
                        .WithMany("Users")
                        .HasForeignKey("UserFavoriteId");
                });
#pragma warning restore 612, 618
        }
    }
}
