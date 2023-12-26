using Microsoft.AspNetCore.Mvc;
using RelationShipProjectMvc.DataContext;

namespace RelationShipProjectMvc.Controllers
{
    public class CourseController : Controller
    {
        private readonly MvcDbContext mvcDbContext;

        public CourseController(MvcDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }
        public IActionResult Index()
        {
            var course = mvcDbContext.Courses.ToList();
            var student = mvcDbContext.Students.ToList();
            var courseStudent = student.GroupJoin(course,  //inner sequence
                                 student => student.CourseRefId, //outerKeySelector 
                                 course => course.Id,     //innerKeySelector
                                 (student, course) => new // resultSelector 
                                 {
                                     StudentName = student.Name,
                                     CourseId = student.CourseRefId,
                                     CourseName = course.ToList().First().Name,

                                 }).ToList();
            ViewBag.CourseStudentList = courseStudent;
            return View();
        }


    }
}
