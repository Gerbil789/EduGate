using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC;
using Database.Models;
using System.Text.RegularExpressions;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly DatabaseService databaseService;

        public LoginController(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var student = new Student();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Student student)
        {
            if(string.IsNullOrEmpty(student.Email) || !Regex.IsMatch(student.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return View(student);
            }

            if (string.IsNullOrEmpty(student.Password) || !Regex.IsMatch(student.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$"))
            {
                return View(student);
            }


            if (!await databaseService.Login(student.Email, student.Password, HttpContext))
            {
                ViewBag.Error = "Incorrect email or password";
                return View(student);
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var student = new Student();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Student student)
        {
            if (string.IsNullOrEmpty(student.Email) || !Regex.IsMatch(student.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return View(student);
            }

            if (string.IsNullOrEmpty(student.Password) || !Regex.IsMatch(student.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$"))
            {
                return View(student);
            }

            if (!await databaseService.Register(student, HttpContext))
            {
                ViewBag.Error = "Registration failed";
                return View(student);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            databaseService.Logout(HttpContext);
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Success()
        {
            return View();
        }


    }
}
