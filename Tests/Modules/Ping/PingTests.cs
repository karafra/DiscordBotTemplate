using Xunit;
using Bot.Modules;
using System.Threading.Tasks;
namespace Tests.Modules;
/// <summary>
/// Ping module tests.
/// </summary>
public class PingTests {
  /// <summary>
  /// Should ping.
  /// </summary>
  [Fact]
  public void ShouldRespondWithPing()
  {
    // Given
    // When
    Task task = new PingModule().Ping();
    // Then
    Assert.False(task.IsCanceled);
    Assert.False(task.IsCompletedSuccessfully);
  }
}