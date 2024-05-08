using System.Collections.Concurrent;
using System.IO;
using System.Windows;
using System.Windows.Controls;

public class TextLogger
{
    private readonly BlockingCollection<string> logQueue = new BlockingCollection<string>();
    private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private readonly Task logWorker;
    private readonly Action<string> onLogAdded;

    public TextLogger(string logFilePath, Action<string> onLogAdded = null)
    {
        this.onLogAdded = onLogAdded;

        logWorker = Task.Run(async () =>
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested || !logQueue.IsCompleted)
                {
                    try
                    {
                        string logMessage;
                        if (logQueue.TryTake(out logMessage, 100, cancellationTokenSource.Token))
                        {
                            await writer.WriteLineAsync(logMessage);
                            await writer.FlushAsync(); // Ensure it's written to disk
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Normal when cancellation is requested
                    }
                    catch
                    {
                        MessageBox.Show("Failed to write log message to file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        });
    }

    public void Log(string message)
    {
        logQueue.Add($"[{DateTime.Now}] {message}");
    }

    public void Stop()
    {
        logQueue.CompleteAdding();
        cancellationTokenSource.Cancel();
        logWorker.Wait(); // Wait for the worker to complete
    }
}
