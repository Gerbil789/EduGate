namespace WebApp
{
  public class TxtLogger : IMyLogger
  {
    public async Task Log(string message)
    {
      await File.AppendAllTextAsync("log.txt", $"{DateTime.Now} - {message}\n");
    }
  }
}
