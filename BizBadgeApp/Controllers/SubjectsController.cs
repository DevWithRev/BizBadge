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
        [HttpGet]
        public IActionResult UpdateSubject(int id) 
        {
            string con = _connection.GetConnectionStrig();
            SubjectsRepo repo = new SubjectsRepo();
            SubjectModel subject = repo.GetSubjectDataById(id, con);
            return View("UpdateSubject",subject);
        }
        [HttpPost]
        public IActionResult UpdateSubject(SubjectModel Subject)
        {
            string con = _connection.GetConnectionStrig();
            SubjectsRepo repo = new SubjectsRepo();
            SubjectModel subject = repo.UpdateSubject(Subject, con);
            if(subject.result > 0) 
            {
                ViewBag.Message = "Update Subject Successful";
                return View("UpdateSubject", subject);
            }
            else
            {
                ViewBag.ErrorMessage = subject.ErrorMessage;
                return View("UpdateSubject", subject);
            }
                
        }
        [HttpPost]
        public IActionResult DeleteSubject(int Id)
        {
            string con = _connection.GetConnectionStrig();
            SubjectsRepo repo = new SubjectsRepo();
            SubjectModel subject = repo.DeleteSubject(Id, con);

            if (subject.result > 0)
            {
                TempData["Success"] = "Subject deleted successfully.";
                return RedirectToAction("SubjectsList");
            }
            else
            {
                TempData["Error"] = subject.ErrorMessage;
                return RedirectToAction("SubjectsList");
            }
        }


    }
}
