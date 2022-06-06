﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AwesomeTomatoes.BLL.Models;

namespace WebAwesomeTomatoes.Models;

public class EFContext : IdentityDbContext<IdentityUser>
{
    public string connectionString;

    public EFContext(DbContextOptions<EFContext> options) : base(options) 
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<MovieBlob> MovieBlobs { get; set; }
    /*protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        modelBuilder.Entity<Movie>()
            .ToTable("Movie")
            .HasKey(p => p.Id);

        modelBuilder.Entity<Movie>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Movie>()
            .Property(p => p.Name)
            .HasColumnType("VARCHAR(40)")
            .IsRequired();

        modelBuilder.Entity<Movie>()
            .Property(p => p.ReleaseDate)
            .HasColumnType("DATETIME")
            .IsRequired();

        modelBuilder.Entity<Movie>()
            .Property(p => p.BoxOffice)
            .HasColumnType("DECIMAL")
            .IsRequired();

        modelBuilder.Entity<Review>()
            .ToTable("Review")
            .HasKey(p => p.Id);

        modelBuilder.Entity<Review>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Review>()
            .Property(p => p.TextReview)
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        modelBuilder.Entity<Review>()
            .Property(p => p.ReviewDate)
            .HasColumnType("DATETIME")
            .IsRequired();

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Reviews)
            .WithOne(p => p.Movie)
            .HasForeignKey(p => p.MovieId);
    }*/
}
