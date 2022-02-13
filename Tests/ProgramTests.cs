using Xunit;
using Bot;
using System.Threading.Tasks;

namespace Tests.Bot;

/// <summary>
/// Tests for program class
/// </summary>
public class ProgramTests
{
  /// <summary>
  /// Should run Main method
  /// </summary>
  [Fact]
  public void ShouldRunMain()
  {
    // Given
    // When
    Task task = Program.Main(new string[] { "" });
    // Then
    Assert.NotNull(task);
    Assert.False(task.IsCanceled);
    Assert.False(task.IsCompleted);
  }
}