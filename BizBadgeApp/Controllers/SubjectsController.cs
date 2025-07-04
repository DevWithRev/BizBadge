using AspNetCoreGeneratedDocument;
using BizBadgeApp.Helper;
using BizBadgeApp.Models;
using BizBadgeApp.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BizBadgeApp.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly DbConnectionStringcs _connection;
        public SubjectsController(DbConnectionStringcs connection)
        {
            _connection = connection;
        }
        [HttpGet]
        public IActionResult SubjectsList ()
        {
            // This action method will return the view for the list of subjects.

            string conn = _connection.GetConnectionStrig();
            SubjectsRepo subjectsRepo = new SubjectsRepo();
            List<SubjectModel> subjectsList = subjectsRepo.GetAllSubjects(conn);
            if(subjectsList != null)
            {
                ViewBag.SubjectsList = subjectsList;
                ViewBag.SubjectsCount = subjectsList.Count;
                return View("SubjectsList", subjectsList);

            }
            else
            {
                ViewBag.ErrorMessage = "No subjects found or an error occurred while retrieving subjects.";
                return View("SubjectsList", subjectsList);
            }

        }
        [HttpGet]
        public IActionResult AddSubject() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSubject(SubjectModel subject) 
        {
            string conn = _connection.GetConnectionStrig();
            SubjectsRepo repo = new SubjectsRepo();
            int result = repo.AddSubject(subject, conn);
            if (result > 0)
            {
                ViewBag.Success = "Subject added successfully.";
                return View();
            }
            else
            {
                ViewBag.Error = "Failed to add subject. Please try again.";
                return View();
            }
        }
    }
}
