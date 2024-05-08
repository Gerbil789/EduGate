using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplicationMVC.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly DatabaseService databaseService;
        public ApplicationController(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSchools(string schoolName)
        {
            var schools = (await databaseService.GetSchoolsAsync()).Select(x => new {x.SchoolId, x.Name}).Where(x => x.Name.Contains(schoolName, StringComparison.OrdinalIgnoreCase)).ToList();
            return Json(schools);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("LoggedUserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var student = await databaseService.GetStudent(userId.Value);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Student student)
        {
            if (!ModelState.IsValid)
            {
               return View(student);
            }

            if (!await databaseService.UpdateStudent(student))
            {
                ViewBag.Error = "An error occurred";
                Console.WriteLine("An error occurred");
                return View(student);
            }

            return RedirectToAction("Success");
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}
