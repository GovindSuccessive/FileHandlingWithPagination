namespace RelationShipProjectMvc.Models
{
    public class AddStudent
    {
        public string Name { get; set; }

        public IFormFile ImagePath { get; set; }
        public Course Course { get; set; }

        public int CourseRefId { get; set; }
    }
}
