using BizBadgeApp.Helper;
using BizBadgeApp.Models;
using BizBadgeApp.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BizBadgeApp.Controllers
{
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
            string connectionString = _connection.GetConnectionStrig();
            // Here you can use the connection string to interact with the database if needed.
            ClassesRepo classesRepo = new ClassesRepo();
            List<ClassesModel> classesList = classesRepo.GetAllClasses(connectionString);
            return View("StudentPage",classesList);
            // This method handles GET requests for the Student page.
            // This action method will return the view for the Student page.

            
        }
        [HttpGet]
        public IActionResult StudentDetils() 
        {
            return View();
        }
    }
}
