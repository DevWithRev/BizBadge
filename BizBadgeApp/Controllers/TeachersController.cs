using Microsoft.AspNetCore.Mvc;

namespace BizBadgeApp.Controllers
{
    public class TeachersController : Controller
    {
        private readonly DBContext _context;
        public TeachersController(DBContext context)
        {
            _context = context;
        }
        public IActionResult ListOfTeachers()
        {
            string conn = _context.Database.GetDbConnection().ConnectionString;
            


        }
    }
}
