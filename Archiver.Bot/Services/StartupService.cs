using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Bot.Exceptions;

namespace Bot.Services;
/// <summary>
/// Class used for launching bot.
/// </summary>
public class StartupService
{
  private readonly IServiceProvider _provider;
  private readonly DiscordSocketClient _discord;
  private readonly CommandService _commands;
  private readonly IConfigurationRoot _config;
  /// <summary>
  /// Constructor for runner service.
  /// </summary>
  /// <param name="provider">Provider of other services (autowiring)</param>
  /// <param name="discord">Discord client socket</param>
  /// <param name="commands">Service handling dispatching of commands</param>
  /// <param name="config">Configuration service</param>
  public StartupService(
      IServiceProvider provider,
      DiscordSocketClient discord,
      CommandService commands,
      IConfigurationRoot config)
  {
    _provider = provider;
    _config = config;
    _discord = discord;
    _commands = commands;
  }
  /// <summary>
  /// Method used for starting starting discord bot. 
  /// </summary>
  /// <returns>Task running discord bot</returns>
  /// <exception cref="MissingConfigException">If discord token is empty or missing</exception>
  public async Task StartAsync()
  {
    string discordToken = _config["tokens:discord"];

    if (string.IsNullOrWhiteSpace(discordToken))
    {
      throw new MissingConfigException();
    }
    await _discord.LoginAsync(TokenType.Bot, discordToken);
    await _discord.StartAsync();
    await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
  }
}
