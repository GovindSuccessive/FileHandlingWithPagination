using Microsoft.EntityFrameworkCore;
using RelationShipProjectMvc.Configuration;
using RelationShipProjectMvc.Models;
using System.Data;

namespace RelationShipProjectMvc.DataContext
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.ApplyConfiguration(new StudentConfigure());
            modelBuilder.ApplyConfiguration(new CourseConfigure());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }


    }
}
