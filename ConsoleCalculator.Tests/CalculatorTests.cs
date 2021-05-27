using Xunit;

namespace ConsoleCalculator.Tests
{
	public class CalculatorTests
	{
		[Fact]
		public void ShouldDoAddOperation()
		{
			Assert.Equal(123.456, Calculator.DoOperation(123, 0.456, "a"));
		}

		[Fact]
		public void ShouldDoSubstractOperation()
		{
			Assert.Equal(1.1, Calculator.DoOperation(2.1, 1, "s"));
		}

		[Fact]
		public void ShouldDoMultiplyOperation()
		{
			Assert.Equal(24.4, Calculator.DoOperation(6.1, 4, "m"));
		}

		[Fact]
		public void ShouldDoDivideOperation()
		{
			Assert.Equal(2, Calculator.DoOperation(6, 3, "d"));
		}

		[Fact]
		public void ShouldFailDivideByZeroOperation()
		{
			Assert.Equal(double.NaN, Calculator.DoOperation(9.6, 0, "d"));
		}

		[Fact]
		public void ShouldFailUnsupportedOperation()
		{
			Assert.Equal(double.NaN, Calculator.DoOperation(1, 2, ""));
		}
	}
}
