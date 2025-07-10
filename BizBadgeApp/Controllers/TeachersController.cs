using BizBadgeApp.Helper;
using BizBadgeApp.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BizBadgeApp.Controllers
{
    public class TeachersController : Controller
    {
        private readonly DbConnectionStringcs _connection;
        public TeachersController(DbConnectionStringcs connection)
        {
            _connection = connection;
        }
        [HttpGet]
        public IActionResult ListOfTeachers()
        {
            string conn = _connection.GetConnectionStrig();
            TeacherRepo teacherRepo = new TeacherRepo();
            var teachers = teacherRepo.GetTeachers(conn);
            if (teachers == null || teachers.Count == 0)
            {
                ViewBag.Message = "No teachers found.";
            }
            else
            {
                ViewBag.Message = "List of Teachers";
                return View("TeachersList", teachers);
            }
            return View("Error"); // Return an error view if no teachers found
        }

        
    }
}
