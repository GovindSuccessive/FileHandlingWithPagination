using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelationShipProjectMvc.Models;

namespace RelationShipProjectMvc.Configuration
{
    public class CourseConfigure: IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany(c => c.Students)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseRefId)
                .HasPrincipalKey(c => c.Id);
        }
    }
}
