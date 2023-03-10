using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.ApiDatabase.DomainModels;

namespace TodoApp.ApiDatabase
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        public ApiDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);
        }
    }
}
