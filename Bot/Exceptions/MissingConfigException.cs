namespace Bot.Exceptions;
/// <summary>
/// Exception thrown if config file or property in config file is missing.
/// </summary>
public class MissingConfigException : Exception
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public MissingConfigException() : base("Please enter your discord bot token into `_config.yml` file in the root of your application") { }
  /// <summary>
  /// Thrown when property or entire config file is missing.
  /// </summary>
  /// <param name="message"> Message of exception </param>
  public MissingConfigException(string message) : base(message) { }
  /// <summary>
  /// Thrown when property or entire config file is missing. 
  /// </summary>
  /// <param name="message">Message of exception.</param>
  /// <param name="inner">Inner exception.</param>
  public MissingConfigException(string message, Exception inner) : base(message, inner) { }
}
