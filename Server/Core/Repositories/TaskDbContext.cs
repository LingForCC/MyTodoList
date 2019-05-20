using System;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .Property(t => t.Name)
                .IsRequired();
        }
    }
}
