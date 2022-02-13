using Xunit;
using Bot.Exceptions;
namespace Tests.Bot.Exceptions;

/// <summary>
/// Tests form missing config exception.
/// </summary>
public class MissingConfigExceptionTests
{

  /// <summary>
  /// Tests if default message appears if no message is provided.
  /// </summary>
  [Fact]
  public async void ShouldThrowExceptionWithoutMessage()
  {
    // Given
    // When     
    MissingConfigException exception = await Assert.ThrowsAsync<MissingConfigException>(() => throw new MissingConfigException());
    // Then
    Assert.Equal("Please enter your discord bot token into `_config.yml` file in the root of your application", exception.Message);
  }
  /// <summary>
  /// Should throw exception with custom message.
  /// </summary>
  [Fact]
  public async void ShouldThrowExceptionWithCustomMessage()
  {
    // Given
    string message = "This is test message";
    // When
    MissingConfigException exception = await Assert.ThrowsAsync<MissingConfigException>(() => throw new MissingConfigException(message));
    // Then
    Assert.Equal(exception.Message, message);
  }
  /// <summary>
  /// Should thro excpetion with inner exception.
  /// </summary>
  /// <exception cref="MissingConfigException">Never.</exception>
  [Fact]
  public async void ShouldThrowExceptionWithInnerException()
  {
    // Given
    string message = "This is test";
    MissingConfigException ex = new MissingConfigException();
    // When
    MissingConfigException exception = await Assert.ThrowsAsync<MissingConfigException>(() => throw new MissingConfigException(message, ex));
    // Then
    Assert.Equal(ex.Message, exception?.InnerException?.Message);
    Assert.Equal(message, exception?.Message);
    Assert.Equal(ex, exception?.InnerException);
  }
}
