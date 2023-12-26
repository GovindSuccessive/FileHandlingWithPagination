namespace RelationShipProjectMvc.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string ImagePath { get; set; }
    }
}
