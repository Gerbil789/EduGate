namespace WebApp
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      //builder.Services.AddSingleton<ErrorHandler>();
      //builder.Services.AddScoped<ErrorHandler>();
      //builder.Services.AddTransient<ErrorHandler>();

      builder.Services.AddScoped<ErrorHandler>();
      builder.Services.AddScoped<IMyLogger, TxtLogger>();

      builder.Services.AddScoped<DataService>();

      var app = builder.Build();

      app.UseMiddleware<ErrorMiddleware>();
      app.UseMiddleware<AuthMiddleware>();
      app.UseMiddleware<FileMiddleware>();
      app.UseMiddleware<FirstPageMiddleware>();
      app.UseMiddleware<SecondPageMiddleware>();
      app.UseMiddleware<FormMiddleware>();
     

      app.Run();
    }
  }
}

