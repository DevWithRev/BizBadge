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
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel user)
        {
            string ConnectionString = _connection.GetConnectionStrig();
            LoginValidate userRepo = new LoginValidate();
            bool isValidUser = userRepo.IsUserExist(user,ConnectionString);
            if (isValidUser) 
            {
                // User is valid, redirect to the dashboard or home page
                ViewData["Message"] = "Login Successful";
                return RedirectToAction("Index", "Home");
            }                               
            else
            {
                // Invalid user, show error message
                ViewData["Message"] = "Invalid email or password.";
                return View("LoginPage", user); // Keep user input on screen
            }   
            
        }

    }
}
