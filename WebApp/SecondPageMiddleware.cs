namespace WebApp
{
  public class SecondPageMiddleware
  {
    public readonly RequestDelegate _next;

    public SecondPageMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      if (context.Request.Path == "/second")
      {
        throw new NotImplementedException("Not implemented yet!");
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync("<h1>SECOND PAGE >_< !</h1>");
      }
      else
      {
        await _next(context);
      }
    }
  }
}
