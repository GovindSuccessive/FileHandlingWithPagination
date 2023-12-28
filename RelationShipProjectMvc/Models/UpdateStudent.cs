namespace RelationShipProjectMvc.Models
{
    public class UpdateStudent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile ImagePath { get; set; }

        public int CourseRefId { get; set; }
    }
}
