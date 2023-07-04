using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Models;

namespace StudentRegistration.Controllers
{
    public class StudentController : Controller
    {
        
        public ActionResult Index()
        {
            // Retrieve all registered students from the database
            List<Student> studentList = Student.GetStudents();
            return View(studentList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                Student.AddStudent(student);
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public ActionResult Clear()
        {
            ModelState.Clear();
            return RedirectToAction("Create");
        }
    }
}
