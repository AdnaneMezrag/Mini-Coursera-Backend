using System;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class MiniCourseraContext:DbContext
    {
        // DbSet properties for your entities
        public DbSet<Domain.Entities.clsCourse> Courses { get; set; }
        public DbSet<Domain.Entities.clsStudent> Students { get; set; }
        public DbSet<Domain.Entities.clsInstructor> Instructors { get; set; }


        // Constructor
        public MiniCourseraContext(DbContextOptions<MiniCourseraContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API example (optional)
            //modelBuilder.Entity<Customer>()
            //    .HasMany(c => c.Orders)
            //    .WithOne(o => o.Customer)
            //    .HasForeignKey(o => o.CustomerId);
        }
    }
}
