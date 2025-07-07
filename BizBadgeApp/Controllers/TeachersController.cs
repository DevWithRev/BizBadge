using Microsoft.AspNetCore.Mvc;

namespace BizBadgeApp.Controllers
{
    public class TeachersController : Controller
    {
        public IActionResult ListOfTeachers()
        {
            return View();
        }
    }
}
