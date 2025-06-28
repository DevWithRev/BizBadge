using BizBadgeApp.Helper;
using BizBadgeApp.Models;
using BizBadgeApp.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BizBadgeApp.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly DbConnectionStringcs _connection;
        public RegistrationController(DbConnectionStringcs connection)
        {
            _connection = connection;
        }
        [HttpGet]
        public IActionResult RegistrationPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegistrationModel UserData)
        {
            if(UserData.Password != UserData.ConfirmPassword) 
            {
                ViewData["Message"] = "Password doesn't Match with confirm Password";
                return View("RegistrationPage",UserData); 
            }
            UserRegistration userRepo = new UserRegistration();
            string conn = _connection.GetConnectionStrig();

            bool userAlreadyExist = userRepo.IsUserExist(UserData.Email, UserData.PhoneNumber, conn);

            if (userAlreadyExist)
            {
                ViewData["Message"] = "Email or Phone number already exists.";
                return View("RegistrationPage",UserData); 
            }
            int result = userRepo.InsertUserDetails(UserData, conn);
            if (result>=1)
            {
                ViewData["Message"] = "Rigerster Succesfull";
                return View("RegistrationPage"); 
            }



            ViewData["Message"] = "Registration successful!";
            return RedirectToAction("Login"); 
        }

    }
}
