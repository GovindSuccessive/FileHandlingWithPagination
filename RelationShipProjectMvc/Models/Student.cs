using System.ComponentModel.DataAnnotations;

namespace RelationShipProjectMvc.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; } 
        public virtual Course Course { get; set; }

        public int CourseRefId { get; set; }
    }
}
