using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Services;

/// <summary>
/// Class handling dispatching of commands to their respective handler methods.
/// </summary>
public class CommandHandler
{
  /// <summary>
  /// Client instance.
  /// </summary>
  private readonly DiscordSocketClient _discord;
  /// <summary>
  /// Command handlers.
  /// </summary>
  private readonly CommandService _commands;
  /// <summary>
  /// Configuration service
  /// </summary>
  private readonly IConfigurationRoot _config;
  /// <summary>
  /// Provider of other services
  /// </summary>
  private readonly IServiceProvider _provider;
  /// <summary>
  /// Service handling disaptching of commands
  /// </summary>
  /// <param name="discord">Discord client</param>
  /// <param name="commands">Autowired commands</param>
  /// <param name="config">Configuration</param>
  /// <param name="provider">Other services</param>
  public CommandHandler(
      DiscordSocketClient discord,
      CommandService commands,
      IConfigurationRoot config,
      IServiceProvider provider)
  {
    _discord = discord;
    _commands = commands;
    _config = config;
    _provider = provider;
    // Add implementation of handler.
    _discord.MessageReceived += OnMessageReceivedAsync;
  }
  private async Task OnMessageReceivedAsync(SocketMessage s)
  {
    var msg = s as SocketUserMessage;
    if (msg is null)
    {
      return;
    }
    if (msg.Author.Id == _discord.CurrentUser.Id)
    {
      return;
    }
    var context = new SocketCommandContext(_discord, msg);
    int argPos = 0;
    if (msg.HasStringPrefix(_config["prefix"], ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
    {
      var result = await _commands.ExecuteAsync(context, argPos, _provider);
      if (!result.IsSuccess)
      {
        await context.Channel.SendMessageAsync(result.ToString());
      }
    }
  }
}
