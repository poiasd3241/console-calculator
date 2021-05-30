using System;

namespace ConsoleCalculator
{
	/// <summary>
	/// <see cref="Console"/> implementation of <see cref="ITextIO"/>.
	/// </summary>
	public class ConsoleTextIO : ITextIO
	{
		public string ConfirmInputLineCommandName => "Enter";

		public string ReadLine()
		{
			return Console.ReadLine();
		}

		public void WriteLine(string input)
		{
			Console.WriteLine(input);
		}

		public void Write(string input)
		{
			Console.Write(input);
		}
	}
}
