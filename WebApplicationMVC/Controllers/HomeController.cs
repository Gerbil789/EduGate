using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
  public class HomeController : BaseController
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, CartService cartService) : base(cartService)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
