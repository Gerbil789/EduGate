using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;

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
      var schools = (await databaseService.GetSchoolsAsync()).Select(x => new { x.SchoolId, x.Name }).Where(x => x.Name.Contains(schoolName, StringComparison.OrdinalIgnoreCase)).ToList();
      return Json(schools);
    }

    [HttpGet]
    public async Task<IActionResult> GetPrograms(int schoolId, string programName)
    {
      var school = (await databaseService.GetSchoolsAsync()).FirstOrDefault(x => x.SchoolId == schoolId);
      if (school == null)
      {
        return Json(new List<SelectListItem>());
      }
      else
      {
        var programs = school.StudyPrograms.Select(x => new SelectListItem(x.Name, x.StudyProgramId.ToString())).Where(x => x.Text.Contains(programName, StringComparison.OrdinalIgnoreCase)).ToList();
        return Json(programs);
      }
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

      await setViewBag(student);
      return View(student);
    }

    async Task setViewBag(Student student)
    {
      var schools = await databaseService.GetSchoolsAsync();

      string[] schoolNames = ["", "", ""];
      string[] programs = ["", "", ""];
      int i = 0;
      foreach (var application in new Application[] { student.Application1, student.Application2, student.Application3 })
      {
        if (application.StudyProgram != null)
        {
          var school = schools.FirstOrDefault(x => x.StudyPrograms.Contains(application.StudyProgram));
          if (school != null)
          {
            programs[i] = application.StudyProgram.Name;
            schoolNames[i] = school.Name;
          }

        }
        i++;
      }
      ViewBag.Schools = schoolNames;
      ViewBag.Programs = programs;
    }

    [HttpPost]
    public async Task<IActionResult> Index(Student student, string[] school, string[] program)
    {
      ModelState.Remove(nameof(Student.StudentId));
      ModelState.Remove(nameof(Student.Password));
      ModelState.Remove(nameof(Student.ApplicationCount));
     

      if (!ModelState.IsValid)
      {
        ViewBag.Schools = school;
        ViewBag.Programs = program;
        return View(student);
      }



      var userId = HttpContext.Session.GetInt32("LoggedUserId");
      student.StudentId = userId.Value;

      var schools = await databaseService.GetSchoolsAsync();

      var school1 = schools.FirstOrDefault(x => x.Name == school[0]);
      if(school1 != null)
      {
        var program1 = school1.StudyPrograms.FirstOrDefault(x => x.Name == program[0]);
        if(program1 != null)
        {
          student.Application1.StudyProgram = program1;
          student.Application1.SubmissionDate = DateTime.Now;
        }
      }

      var school2 = schools.FirstOrDefault(x => x.Name == school[1]);
      if (school2 != null)
      {
        var program2 = school2.StudyPrograms.FirstOrDefault(x => x.Name == program[1]);
        if (program2 != null)
        {
          student.Application2.StudyProgram = program2;
          student.Application2.SubmissionDate = DateTime.Now;
        }
      }

      var school3 = schools.FirstOrDefault(x => x.Name == school[2]);
      if (school3 != null)
      {
        var program3 = school3.StudyPrograms.FirstOrDefault(x => x.Name == program[2]);
        if (program3 != null)
        {
          student.Application3.StudyProgram = program3;
          student.Application3.SubmissionDate = DateTime.Now;
        }
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
