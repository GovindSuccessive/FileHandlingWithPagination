using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelationShipProjectMvc.Models;

namespace RelationShipProjectMvc.Configuration
{
    public class StudentConfigure:IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(s => s.Course)
               .WithMany(c => c.Students)
               .HasForeignKey(s => s.CourseRefId)
               .HasPrincipalKey(c => c.Id);

            builder.Navigation(s => s.Course)
               .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
