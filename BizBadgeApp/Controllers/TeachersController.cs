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

        public IActionResult Delete(int TeacherId)
        {
            string conn = _connection.GetConnectionStrig();
            TeacherRepo teacherRepo = new TeacherRepo();
            int rowsAffected = teacherRepo.Delete(TeacherId, conn);
            if (rowsAffected > 0)
            {
                TempData["Message"] = "Teacher deleted successfully.";
            }
            else
            {
                TempData["Message"] = "Error deleting teacher.";
            }
            return ListOfTeachers();

        }
    }
}
