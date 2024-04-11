using System.Net;

namespace WebApp
{
  public class FormMiddleware
  {
    private readonly RequestDelegate _next;
    public FormMiddleware(RequestDelegate next)
    {
      _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
      if (context.Request.Path == "/form")
      {
        if(context.Request.Method == "POST")
        {
          context.Response.ContentType = "text/html; charset=utf-8";
          var msg = context.Request.Form["msg"];
          await context.Response.WriteAsync($"<h1>Message: {WebUtility.HtmlEncode(msg)}</h1>");
          return;
        }

        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(@"<form method='POST'><input type='text' name='msg' /><input type='submit' /></form>");
        return;
      }
    
      await _next(context);
      
    }
  }
}
