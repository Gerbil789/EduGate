using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationMVC.Controllers
{
  public abstract class BaseController : Controller
  {
    protected readonly CartService CartService;
    public BaseController(CartService cartService)
    {
      CartService = cartService;
    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      ViewBag.CartProductCount = CartService.Count();
      base.OnActionExecuting(context);
    }
  }
}
