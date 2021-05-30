using Xunit;

namespace ConsoleCalculator.Tests
{
	public class ConsoleTextIOTests
	{
		[Fact]
		public void ShouldCreateConsoleTextIO()
		{
			ConsoleTextIO consoleTextIO = new();
			Assert.Equal("Enter", consoleTextIO.ConfirmInputLineCommandName);
		}
	}
}
