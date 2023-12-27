using System.ComponentModel.DataAnnotations;

namespace RelationShipProjectMvc.Models
{
    public class AddStudent
    {
        [Required(ErrorMessage ="Name is Required")]
        [StringLength(50,ErrorMessage ="Name should be lesser then 50 Characters")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Image is Required")]
        
        public IFormFile ImagePath { get; set; }
        public Course Course { get; set; }

        [Required(ErrorMessage ="Course is Required")]
        public int CourseRefId { get; set; }
    }
}
