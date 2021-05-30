using System;
using System.Collections.Generic;

namespace ConsoleCalculator
{
	class CalculatorPresenter
	{
		#region Private Properties

		private readonly ICalculator _calculator;
		private readonly ITextIO _textIO;
		private readonly string _calculatorName;

		private static readonly List<string> _allowedOperations = new() { "a", "s", "m", "d" };

		#endregion

		#region Constructor

		public CalculatorPresenter(ICalculator calculator, ITextIO textIO, string calculatorName = "Console Calculator")
		{
			_calculator = calculator;
			_textIO = textIO;
			_calculatorName = calculatorName;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Launches the calculator app aimed at a <see cref="ITextIO"/> environment.
		/// </summary>
		public void RunCalculatorTextIO()
		{
			// Display title as the C# calculator app.
			_textIO.WriteLine($"{_calculatorName} in C#\r");
			_textIO.WriteLine("------------------------");
			_textIO.WriteLine($"Note: all input must be confirmed by '{_textIO.ConfirmInputLineCommandName}'.\n");

			while (RunCalculationSequence())
			{
				// Friendly line spacing between sequences.
				_textIO.WriteLine("\n");
			}

			return;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gathers the valid user input for the operation and then performs it, displaying the result.
		/// </summary>
		/// <returns><see langword="false"/> if the user wants to exit the app;
		/// otherwise, <see langword="true"/> (user wants to perform another operation).
		/// </returns>
		private bool RunCalculationSequence()
		{
			// Ask the user to type the first number.
			_textIO.Write($"Type a number: ");
			double number1 = ObtainValidDouble(_textIO.ReadLine());

			// Ask the user to type the second number.
			_textIO.Write($"Type another number: ");
			double number2 = ObtainValidDouble(_textIO.ReadLine());

			// Ask the user to choose an operation.
			_textIO.WriteLine($"Choose an operation from the following list:");
			_textIO.WriteLine("\ta - Add");
			_textIO.WriteLine("\ts - Subtract");
			_textIO.WriteLine("\tm - Multiply");
			_textIO.WriteLine("\td - Divide");
			_textIO.Write("Your option? ");

			string op = ObtainValidOperation(_textIO.ReadLine());

			// Perform the operation.
			_textIO.WriteLine(OperationResult(number1, number2, op));

			_textIO.WriteLine("------------------------\n");

			// Wait for the user to respond before closing.
			_textIO.Write($"Type 'n' to close the app, or any other key to continue: ");
			return _textIO.ReadLine() != "n";
		}

		/// <summary>
		/// Obtains the valid number (int or double) from the user,
		/// then returns this number.
		/// </summary>
		/// <param name="input">The initial user input to be validated as an operation.</param>
		/// <returns></returns>
		private double ObtainValidDouble(string input)
		{
			double result;
			while (double.TryParse(input, out result) == false)
			{
				_textIO.Write("This is not a valid input. Please enter a double or integer value: ");
				input = _textIO.ReadLine();
			}
			return result;
		}

		/// <summary>
		/// Obtains the valid operation (<see cref="_allowedOperations"/>) from the user,
		/// then returns this operation.
		/// </summary>
		/// <param name="input">The initial user input to be validated as an operation.</param>
		/// <returns></returns>
		private string ObtainValidOperation(string input)
		{
			while (_allowedOperations.Contains(input) == false)
			{
				_textIO.Write("This is not a valid input. Please enter a valid operation: ");
				input = _textIO.ReadLine();
			}
			return input;
		}

		/// <summary>
		/// Returns the message containing the result of the specified calculation operation,
		/// if it succeeds; otherwise, a warning message or error details.
		/// <br/>
		/// Syntax (usage): [number1] [operation] [number2].
		/// </summary>
		/// <param name="number1">The first number participating in the operation.</param>
		/// <param name="number2">The second number participating in the operation.</param>
		/// <param name="operation">The mathematical operation to perform.</param>
		private string OperationResult(double number1, double number2, string operation)
		{
			try
			{
				var result = _calculator.DoOperation(number1, number2, operation);
				if (double.IsNaN(result))
				{
					return "This operation will result in a mathematical error.\n";
				}
				else
					return string.Format("Your result: {0:0.##}\n", result);
			}
			catch (Exception e)
			{
				return $"Oh no! An exception occurred trying to do the math.\n - Details: {e.Message}\n";
			}
		}

		#endregion
	}
}
