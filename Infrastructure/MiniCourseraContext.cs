using System;
using Infrastructure.Configurations;
using Infrastructure.Configurations.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class MiniCourseraContext:DbContext
    {
        public DbSet<Domain.Entities.Course> Courses { get; set; }
        public DbSet<Domain.Entities.User> Users { get; set; }


        public MiniCourseraContext(DbContextOptions<MiniCourseraContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SubjectConfig());
            modelBuilder.ApplyConfiguration(new LanguageConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());


        }
    }
}
