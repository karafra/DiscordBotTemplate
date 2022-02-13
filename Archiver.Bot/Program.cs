namespace Bot;
/// <summary>
/// Main class running the bot.
/// </summary>
public sealed class Program
{
  /// <summary>
  /// Runner method of bot 
  /// </summary>
  /// <param name="args">Argumets to run bot with.</param>
  /// <returns>Task running bot.</returns>
  public static Task Main(string[] args) => Startup.RunAsync(args);
}
