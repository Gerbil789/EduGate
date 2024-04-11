using System.Runtime.CompilerServices;

namespace WebApp
{
  public class ErrorHandler
  {
    private readonly IMyLogger _logger;
    public ErrorHandler(IMyLogger logger)
    {
      _logger = logger;
    }
    public async Task Handle(Exception ex)
    {
      await _logger.Log($"{ex.GetType()}: {ex.Message}");
    }
  }
}
