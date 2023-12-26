using Microsoft.AspNetCore.Mvc;
using RelationShipProjectMvc.DataContext;
using RelationShipProjectMvc.Models;
using System.Net.Http.Headers;

namespace RelationShipProjectMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly MvcDbContext mvcDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(MvcDbContext mvcDbContext,IWebHostEnvironment webHostEnvironment)
        {
            this.mvcDbContext = mvcDbContext;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public IActionResult Index()
        {
            
            var course = mvcDbContext.Courses.ToList();
            var student = mvcDbContext.Students.ToList();
            var studentCourse = student.Join(
                                              course,
                                              student => student.CourseRefId,
                                              course => course.Id,
                                              (student, course) => new StudentCourse()
                                              {
                                                  Id = student.Id,
                                                  StudentName = student.Name,
                                                  CourseId = course.Id,
                                                  CourseName = course.Name,
                                                  ImagePath = student.ImagePath,

                                              }).ToList();
                
            return View("Index", studentCourse);
        }

        [HttpGet]
        public  IActionResult AddStudent()
        {
            ViewBag.CourseList = mvcDbContext.Courses.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudent addStudent)
        {
            string uniqueFileName = "";
            if (addStudent.ImagePath != null)
            {
                string uploadFoler = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + addStudent.ImagePath.FileName;
                string filePath = Path.Combine(uploadFoler, uniqueFileName);
                addStudent.ImagePath.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            var student = new Student()
            {
                Name = addStudent.Name,
                CourseRefId = addStudent.CourseRefId,
                ImagePath = uniqueFileName,
            };
                mvcDbContext.Students.Add(student);
                await mvcDbContext.SaveChangesAsync();
            ViewBag.Success = "Student Added Successfully";
            /*ViewBag.CourseList = mvcDbContext.Courses.ToList();*/
            // TempData["SuccessMessage"] = "Student Added Successfully.";
            return RedirectToAction("Index");
        }

      
    }
}
