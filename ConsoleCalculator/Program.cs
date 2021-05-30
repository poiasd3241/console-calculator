namespace ConsoleCalculator
{
	class Program
	{
		public static CalculatorPresenter CalcPresenter = new(new Calculator(), new ConsoleTextIO());

		public static void Main(string[] args)
		{
			CalcPresenter.RunCalculatorTextIO();
		}
	}
}
