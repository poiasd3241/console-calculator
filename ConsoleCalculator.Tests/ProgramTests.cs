using Xunit;
using static ConsoleCalculator.Tests.CalculatorPresenterTests;

namespace ConsoleCalculator.Tests
{
	public class ProgramTests
	{
		private readonly CalculatorPresenterTests _calculatorPresenterTests = new();

		[Fact]
		public void ShouldRunMain()
		{
			// Given
			MockConsoleIO mockConsoleIO = _calculatorPresenterTests.GetOneCalculationSequenceAndExitMockConsoleIO();
			Program.CalculatorPresenter = new(new Calculator(), mockConsoleIO);

			var expectedOutput = _calculatorPresenterTests.GetOneCalculationSequenceAndExitOutput();

			// When
			Program.Main(null);

			// Then
			Assert.Equal(expectedOutput, mockConsoleIO.CurrentOutput);
		}
	}
}
