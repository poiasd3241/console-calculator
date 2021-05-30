using Xunit;

namespace ConsoleCalculator.Tests
{
	public class CalculatorTests
	{
		private readonly ICalculator _calculator = new Calculator();

		[Fact]
		public void ShouldDoAddOperation()
		{
			Assert.Equal(123.456, _calculator.DoOperation(123, 0.456, "a"));
		}

		[Fact]
		public void ShouldDoSubstractOperation()
		{
			Assert.Equal(1.1, _calculator.DoOperation(2.1, 1, "s"));
		}

		[Fact]
		public void ShouldDoMultiplyOperation()
		{
			Assert.Equal(24.4, _calculator.DoOperation(6.1, 4, "m"));
		}

		[Fact]
		public void ShouldDoDivideOperation()
		{
			Assert.Equal(2, _calculator.DoOperation(6, 3, "d"));
		}

		[Fact]
		public void ShouldFailDivideByZeroOperation()
		{
			Assert.Equal(double.NaN, _calculator.DoOperation(9.6, 0, "d"));
		}

		[Fact]
		public void ShouldFailUnsupportedOperation()
		{
			Assert.Equal(double.NaN, _calculator.DoOperation(1, 2, ""));
		}
	}
}
