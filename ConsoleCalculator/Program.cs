namespace ConsoleCalculator
{
	class Program
	{
		public static CalculatorPresenter CalculatorPresenter = new(new Calculator(), new ConsoleTextIO());

		public static void Main(string[] args)
		{
			CalculatorPresenter.RunCalculatorTextIO();
		}
	}
}
