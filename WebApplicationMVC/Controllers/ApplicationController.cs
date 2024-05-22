using WebApplicationMVC.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
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
      foreach (var application in student.Applications)
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
      ModelState.Remove(nameof(Student.Applications));
     

      if (!ModelState.IsValid)
      {
        ViewBag.Schools = school;
        ViewBag.Programs = program;
        return View(student);
      }



      var userId = HttpContext.Session.GetInt32("LoggedUserId");
      student.StudentId = userId.Value;

      var schools = await databaseService.GetSchoolsAsync();

      student.Applications = new();
            student.Applications.Add(new Application() { Student = student });
            student.Applications.Add(new Application() { Student = student });
            student.Applications.Add(new Application() { Student = student });

      var school1 = schools.FirstOrDefault(x => x.Name == school[0]);
      if(school1 != null)
      {
        var program1 = school1.StudyPrograms.FirstOrDefault(x => x.Name == program[0]);
        if(program1 != null)
        {
          student.Applications[0].StudyProgram = program1;
          student.Applications[0].Date = DateTime.Now;
        }
            }


      var school2 = schools.FirstOrDefault(x => x.Name == school[1]);
      if (school2 != null)
      {
        var program2 = school2.StudyPrograms.FirstOrDefault(x => x.Name == program[1]);
        if (program2 != null)
        {
          student.Applications[1].StudyProgram = program2;
          student.Applications[1].Date = DateTime.Now;
        }
      }

      var school3 = schools.FirstOrDefault(x => x.Name == school[2]);
      if (school3 != null)
      {
        var program3 = school3.StudyPrograms.FirstOrDefault(x => x.Name == program[2]);
        if (program3 != null)
        {
          student.Applications[2].StudyProgram = program3;
          student.Applications[2].Date = DateTime.Now;
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
