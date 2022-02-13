using Discord.Commands;

namespace Bot.Modules;
/// <summary>
/// Module containing commands for checking bot connection.
/// </summary>
[Name("ping")]
[Summary("Checks if bot responds to commands")]
public class PingModule : ModuleBase<SocketCommandContext>
{
  /// <summary>
  /// Handles commands thats sole purpose is to checks if bot responds to any command.
  /// </summary>
  /// <returns> Task dispatcher with response `Pong!`</returns>
  [Command("ping"), Alias("Ping")]
  [Summary("Check if bot has connected to gateway")]
  public Task Ping() => ReplyAsync("Pong!");
}
