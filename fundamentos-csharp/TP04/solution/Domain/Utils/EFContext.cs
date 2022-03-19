﻿using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Domain.Utils
{
    public class EFContext : DbContext
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=infnet;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Movies>? Movies { get; set; }

    }
}

