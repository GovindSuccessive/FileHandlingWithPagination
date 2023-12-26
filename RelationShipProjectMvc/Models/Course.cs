using System.ComponentModel.DataAnnotations;

namespace RelationShipProjectMvc.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

    }
}
