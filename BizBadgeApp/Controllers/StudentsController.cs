using BizBadgeApp.Helper;
using BizBadgeApp.Models;
using BizBadgeApp.Repos;
using Microsoft.AspNetCore.Mvc;
using BizBadgeApp.Filters; // ✅ Add this

namespace BizBadgeApp.Controllers
{
    [NoCache] // ✅ Prevent browser from caching
    public class StudentsController : Controller
    {
        private readonly DbConnectionStringcs _connection;
        public StudentsController(DbConnectionStringcs connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public IActionResult Student()
        {
            // ✅ Check if session is valid
            if (HttpContext.Session.GetString("Name") == null)
            {
                return RedirectToAction("LoginPage", "Login");
            }

            string connectionString = _connection.GetConnectionStrig();
            ClassesRepo classesRepo = new ClassesRepo();
            List<ClassesModel> classesList = classesRepo.GetAllClasses(connectionString);
            string? name = HttpContext.Session.GetString("Name");
            ViewBag.UserName = name;

            return View("StudentPage", classesList);
        }

        [HttpGet]
        public IActionResult StudentDetils(int id)
        {
            // ✅ Session check
            if (HttpContext.Session.GetString("Name") == null)
            {
                return RedirectToAction("LoginPage", "Login");
            }

            string conn = _connection.GetConnectionStrig();
            StudentsRepo studentsRepo = new StudentsRepo();
            List<StudentModel> ClassWiseStudentList = studentsRepo.GetClassWiseStudentData(id, conn);
            ViewBag.ClassId = id;
            if (ClassWiseStudentList.Any())
                ViewBag.ClassName = ClassWiseStudentList.First().ClassName;
            else
                ViewBag.ClassName = "Unknown";
            return View("ClassWiseStudentsList", ClassWiseStudentList);
        }

        [HttpGet]
        public IActionResult AddStudent(int classId, string className)
        {
            ViewBag.ClassId = classId;
            ViewBag.ClassName = className;
            return View();
        }
        [HttpPost]
        public IActionResult AddStudent(StudentModel NewStudent) 
        {
            string conn = _connection.GetConnectionStrig();
            StudentsRepo repo = new StudentsRepo();
            int result = repo.NewStudetData(NewStudent, conn);
           
            if (result <= 1) 
            {
                ViewBag.ClassId = NewStudent.ClassId;
                ViewBag.ClassName = NewStudent.ClassName;
                ViewData["Succes"] = "New Student Added";
                return View();
            }
            else 
            {
                ViewData["Succes"] = "Error";
                return View();
            }
           
        }
        [HttpGet]
        public IActionResult EditStudent(int id) 
        {
            string conn = _connection.GetConnectionStrig();
            StudentsRepo repo = new StudentsRepo();
            StudentModel student = repo.GetStudentById(id, conn);
            if (student == null)
            {
                return NotFound(); // ✅ Handle not found case
            }

            ViewBag.ClassId = student.ClassId;
            ViewBag.ClassName = student.ClassName;
            return View("EditStudent",student);
        }
        [HttpPost]
        public IActionResult UpdateStudent(StudentModel student)
        {
            string conn = _connection.GetConnectionStrig();
            StudentsRepo repo = new StudentsRepo();
            int result = repo.UpdateStudent(student, conn);
            if (result > 0)
            {
                ViewData["Succes"] = "Student Updated Successfully"; // ✅ Success message
                List<StudentModel> students = repo.GetClassWiseStudentData(student.ClassId, conn);
                return View("ClassWiseStudentsList",students); // ✅ Handle not found case
            }

            ViewBag.ClassId = student.ClassId;
            ViewBag.ClassName = student.ClassName;
            return View("EditStudent");
        }


        [HttpGet]
        public IActionResult DeleteStudent(int id, int classId)
        {
            string conn = _connection.GetConnectionStrig();
            StudentsRepo repo = new StudentsRepo();

            int result = repo.DeleteStudent(id, conn);

            if (result > 0)
            {
                ViewData["Success"] = "Student deleted successfully.";
                List<StudentModel> students = repo.GetClassWiseStudentData(classId, conn);
                return View("ClassWiseStudentsList", students);
            }

            ViewData["Error"] = "Failed to delete student.";
            List<StudentModel> fallbackStudents = repo.GetClassWiseStudentData(classId, conn);
            return View("ClassWiseStudentsList", fallbackStudents);
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // ✅ Clear session
            return RedirectToAction("LoginPage", "Login"); // ✅ Go to login page
        }
    }
}
