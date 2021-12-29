using Microsoft.EntityFrameworkCore;
using PolySondage.Data.Models;
using System;

namespace PolySondage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { set; get; }
        public DbSet<Poll> Polls { set; get; }
        public DbSet<Choice> Choices { set; get; }
        public DbSet<Vote> Votes { set; get; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
              .HasIndex(u => u.Email)
              .IsUnique();
        }
    }
}
