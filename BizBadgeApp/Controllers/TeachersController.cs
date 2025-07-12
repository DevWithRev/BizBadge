using BizBadgeApp.Helper;
using BizBadgeApp.Models;
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
            return View("Error"); 
        }
        [HttpGet]
        public IActionResult AddTeacher()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TeacherDetails(int TeacherId)
        {
            string conn = _connection.GetConnectionStrig();
            TeacherRepo teacherRepo = new TeacherRepo();
            var teachers = teacherRepo.GetTeachers(conn);
            var teacher = teachers.FirstOrDefault(teacher => teacher.TeacherId == TeacherId);
            if (teacher == null)
            {
                ViewBag.Message = "Teacher not found.";
                return View("Error");
            }
            return View("TeacherEdit", teacher);
        }

        [HttpPost]
        //public IActionResult AddTeacher()
        //{

        //    return View();
        //}

        [HttpPost]
        public IActionResult UpdateTeacher(TeacherModel Teacher)
        {
            string conn = _connection.GetConnectionStrig();
            TeacherRepo repo = new TeacherRepo();
            int result = repo.UpadateTeacher(Teacher, conn);
            if(result > 0)
            {
                return ListOfTeachers();
            }
            return TeacherDetails(Teacher.TeacherId);

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
