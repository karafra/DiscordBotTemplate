using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bot.Services;

namespace Bot;
/// <summary>
/// Starter class of bot.
/// </summary>
public class Startup
{ 
  /// <summary>
  /// Configuration object.
  /// </summary>
  public IConfigurationRoot Configuration { get; }

  /// <summary>
  /// Starts bot in sync mode. 
  /// </summary>
  /// <param name="args">Arguments to start bot with.</param>
  public Startup(string[] args)
  {
    var builder = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddYamlFile("_config.yml");
    Configuration = builder.Build();
  }
  /// <summary>
  /// Runs bot in async mode waining indefinitely.
  /// </summary>
  /// <param name="args">arguments to run bot with</param>
  /// <returns>Task running bot</returns>
  public static async Task RunAsync(string[] args)
  {
    var startup = new Startup(args);
    await startup.RunAsync();
  }

  /// <summary>
  /// Runs bot in async mode waining indefinitely.
  /// </summary>
  /// <returns>Task on which bot is running.</returns>
  public async Task RunAsync()
  {
    var services = new ServiceCollection();
    ConfigureServices(services);

    var provider = services.BuildServiceProvider();
    provider.GetRequiredService<LoggingService>();
    provider.GetRequiredService<CommandHandler>();
    await provider.GetRequiredService<StartupService>().StartAsync();
    await Task.Delay(-1);
  }
  private void ConfigureServices(IServiceCollection services)
  {
    services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
    {
      LogLevel = LogSeverity.Verbose,
      MessageCacheSize = 1000
    }))
    .AddSingleton(new CommandService(new CommandServiceConfig
    {
      LogLevel = LogSeverity.Verbose,
      DefaultRunMode = RunMode.Async,
    }))
    .AddSingleton<CommandHandler>()
    .AddSingleton<StartupService>()
    .AddSingleton<LoggingService>()
    .AddSingleton(Configuration);
  }
}
