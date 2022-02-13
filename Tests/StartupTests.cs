using Bot;
using Xunit;
using System.Threading.Tasks;
namespace Tests.Bot;

/// <summary>
/// Unit tests for startup service.
/// </summary>
public class StartupTests
{

  private readonly Startup startup = new Startup(
    new string[] { "" }
  );

  /// <summary>
  /// Should run async without error
  /// </summary>
  [Fact]
  public void ShouldRunAsync()
  {
    // Given
    // When
    Task task = startup.RunAsync();
    // Then
    Assert.NotNull(task);
    Assert.False(task.IsCompleted);
    Assert.False(task.IsCanceled);
  }

  /// <summary>
  /// Should run from static method without error.
  /// </summary>
  [Fact]
  public void ShouldRunAsyncWithArguments()
  {
    // Given
    // When
    Task task = Startup.RunAsync(new string[] { "" });
    // Then
    Assert.NotNull(task);
    Assert.False(task.IsCompleted);
    Assert.False(task.IsCanceled);
  }
}