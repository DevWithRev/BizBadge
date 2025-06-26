using BizBadgeApp.Helper;
using BizBadgeApp.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace BizBadgeApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly DbConnectionStringcs _connection;
        public LoginController(DbConnectionStringcs connection)
        {
            _connection = connection;
        }
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Role,string Email,string Password)
        {
            string ConnectionString = _connection.GetConnectionStrig();
            LoginValidate loginValidate = new LoginValidate();
            loginValidate.IsEmailExist(Email);
            return View();
        }

    }
}
