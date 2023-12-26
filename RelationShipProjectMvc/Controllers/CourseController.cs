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
            return View(course);
        }


    }
}
