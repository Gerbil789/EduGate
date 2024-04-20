using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
  public class EshopController : BaseController
  {
    private readonly ProductService ProductService;
    public EshopController(ProductService productService, CartService cartService) : base(cartService)
    {
      ProductService = productService;
    }

   

    public IActionResult Index()
    {
      ViewBag.Products = this.ProductService.List();
      return View();
    }

    public IActionResult Detail(int id)
    {
      var product = this.ProductService.Find(id);
      if (product == null)
      {
        return NotFound();
      }
      ViewBag.Product = product;
      return View();
    }

    public IActionResult AddToCart(int id)
    {
      CartService.Add(this.ProductService.Find(id));
      return RedirectToAction("Detail", new {id = id});
    }

    public IActionResult Form()
    {
      ViewBag.Products = CartService.List();
      return View();
    }

    [HttpPost]
    public IActionResult Form(OrderForm form)
    {
      if (ModelState.IsValid)
      {
        return RedirectToAction("Success");
      }

      ViewBag.Products = CartService.List();
      return View();
    }

    public IActionResult Success()
    {
      return View();
    }


  }
}
