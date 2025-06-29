using BizBadgeApp.Helper;
using BizBadgeApp.Models;
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
        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginModel user)
        {
            string ConnectionString = _connection.GetConnectionStrig();
            LoginValidate userRepo = new LoginValidate();
            LoginModel foundUser = userRepo.IsUserExist(user,ConnectionString);
            if (foundUser !=null) 
            {
               HttpContext.Session.SetString("Name", foundUser.Name);
               HttpContext.Session.SetString("Email", foundUser.Email);

                ViewData["Message"] = "Login Successful";
                //RedirectToAction("Index", "Home");
                return View("LoginPage",foundUser);
            }                               
            else
            {
                ViewData["Message"] = "Invalid email or password.";
                return View("LoginPage", user); 
            }   
            
        }

    }
}
