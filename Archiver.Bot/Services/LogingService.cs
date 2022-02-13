using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot.Services;
/// <summary>
/// Service handlind loging of events.
/// </summary>
public class LoggingService
{
  private readonly DiscordSocketClient _discord;
  private readonly CommandService _commands;

  private string _logDirectory { get; }
  private string _logFile => Path.Combine(_logDirectory, $"{DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt");

  /// <summary>
  /// Constructor of service handling loging of events.
  /// </summary>
  /// <param name="discord">Discord client.</param>
  /// <param name="commands">Service handling commands to be appended to client.</param>
  public LoggingService(DiscordSocketClient discord, CommandService commands)
  {
    _logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");

    _discord = discord;
    _commands = commands;

    _discord.Log += OnLogAsync;
    _commands.Log += OnLogAsync;
  }

  private Task OnLogAsync(LogMessage msg)
  {
    if (!Directory.Exists(_logDirectory))
    {
      Directory.CreateDirectory(_logDirectory);
    }
    if (!File.Exists(_logFile))
    {
      File.Create(_logFile).Dispose();
    }
    return WriteLog(msg);
  }
  private void ChangeColor(LogSeverity serverity)
  {
    switch (serverity)
    {
      case LogSeverity.Debug:
        Console.ForegroundColor = ConsoleColor.Green;
        return;
      case LogSeverity.Verbose:
        Console.ForegroundColor = ConsoleColor.Cyan;
        return;
      case LogSeverity.Info:
        Console.ForegroundColor = ConsoleColor.Green;
        return;
      case LogSeverity.Warning:
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        return;
      case LogSeverity.Error:
        Console.ForegroundColor = ConsoleColor.Red;
        return;
      case LogSeverity.Critical:
        Console.ForegroundColor = ConsoleColor.Black;
        return;
    }
  }
  private Task WriteLog(LogMessage msg)
  {
    string logLabel = $"{DateTime.UtcNow.ToString("hh:mm:ss")} ";
    string logSeverity = $"[{msg.Severity}]";
    string logMessage = $" {msg.Source}: {msg.Exception?.ToString() ?? msg.Message}";

    Console.Out.WriteAsync(logLabel);
    ChangeColor(msg.Severity);
    Console.Out.Write($" [{msg.Severity}]");
    Console.ResetColor();

    File.AppendAllText(_logFile, logLabel + logMessage + "\n");
    return Console.Out.WriteLineAsync(logMessage);
  }
}
