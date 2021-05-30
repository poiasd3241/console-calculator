namespace ConsoleCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			CalculatorPresenter calculatorPresenter = new(new Calculator(), new ConsoleTextIO());
			calculatorPresenter.RunCalculatorTextIO();
		}
	}
}
