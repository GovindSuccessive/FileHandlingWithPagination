using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var student = mvcDbContext.Students.Include(x => x.Course).ToList();   
            return View("Index", student);
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
           if(ModelState.IsValid)
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
            return RedirectToAction("Index") ;
        }

        [HttpGet]
        public async  Task<IActionResult> View(int id)
        {
            var student = await mvcDbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            ViewBag.CourseList = mvcDbContext.Courses.ToList();

            if (student != null)
            {
                var updatestudent = new UpdateStudent()
                {
                    Id = student.Id,
                    Name = student.Name,
                    CourseRefId = student.CourseRefId,
                   
                };
                return await Task.Run(() => View("View",updatestudent));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateStudent updateStudent)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = "";
                if (updateStudent.ImagePath != null)
                {
                    string uploadFoler = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + updateStudent.ImagePath.FileName;
                    string filePath = Path.Combine(uploadFoler, uniqueFileName);
                    updateStudent.ImagePath.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                var student = await mvcDbContext.Students.FindAsync(updateStudent.Id);

                if (student != null)
                {
                    student.Name = updateStudent.Name;
                    student.ImagePath = uniqueFileName;
                    student.CourseRefId = updateStudent.CourseRefId;
                }
                await mvcDbContext.SaveChangesAsync();
                ViewBag.Success = "Student Updated Successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = mvcDbContext.Students.FirstOrDefault(x => x.Id == id);    
            if (student != null)
            {
                mvcDbContext.Students.Remove(student);
                await mvcDbContext.SaveChangesAsync();  
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
