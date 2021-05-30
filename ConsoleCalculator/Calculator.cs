namespace ConsoleCalculator
{
	public class Calculator : ICalculator
	{
		public double DoOperation(double number1, double number2, string operation)
		{
			var result = double.NaN;

			switch (operation)
			{
				case "a":
					result = number1 + number2;
					break;
				case "s":
					result = number1 - number2;
					break;
				case "m":
					result = number1 * number2;
					break;
				case "d":
					if (number2 != 0)
					{
						result = number1 / number2;
					}
					break;
				default:
					break;
			}
			return result;
		}
	}
}
