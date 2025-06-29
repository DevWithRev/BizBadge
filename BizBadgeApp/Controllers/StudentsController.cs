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

            return View("ClassWiseStudentsList", ClassWiseStudentList);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // ✅ Clear session
            return RedirectToAction("LoginPage", "Login"); // ✅ Go to login page
        }
    }
}
