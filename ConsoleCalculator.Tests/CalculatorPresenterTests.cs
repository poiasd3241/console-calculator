using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsoleCalculator.Tests
{
	public class CalculatorPresenterTests
	{
		#region Private Members

		private readonly Queue<string> _inputForOneCalculationSequenceAndExit;
		private readonly Queue<string> _inputForOneCalculationSequenceAndRestart;
		private readonly Queue<string> _inputForSingleFailAllInput;
		private readonly Queue<string> _inputForOperationFail;
		private readonly Queue<string> _inputForOperationException;

		private static readonly string _titleOutput =
			"Console Calculator in C#\r\n" +
			"------------------------\n" +
			$"Note: all input must be confirmed by 'Enter'.\n\n";

		private static readonly string _availableOperationsDisplay =
			"Choose an operation from the following list:\n" +
			"\ta - Add\n" +
			"\ts - Subtract\n" +
			"\tm - Multiply\n" +
			"\td - Divide\n";

		private static readonly string _validInputDisplayAndEnter =
			"Type a number: 1\n" +
			"Type another number: 2\n" +
			_availableOperationsDisplay +
			"Your option? a\n";

		private static readonly string _exitConfirmedDisplay =
			"------------------------\n\n" +
			"Type 'n' to close the app, or any other key to continue: n\n";

		private static readonly string _restartConfirmedDisplay =
			"------------------------\n\n" +
			"Type 'n' to close the app, or any other key to continue: not n\n";

		private CalculatorPresenter _presenter;

		#endregion

		#region Constructor

		public CalculatorPresenterTests()
		{
			List<string> validInputSequenceExit = new() { "1", "2", "a", "n" };
			List<string> validInputSequenceRestartAndExit = new() { "1", "2", "a", "not n" };
			validInputSequenceRestartAndExit.AddRange(validInputSequenceExit);

			_inputForOneCalculationSequenceAndExit = new(validInputSequenceExit);
			_inputForOneCalculationSequenceAndRestart = new(validInputSequenceRestartAndExit);
			_inputForSingleFailAllInput = new(new string[] { "a", "1", "b", "2", "oops", "a", "n" });
			_inputForOperationFail = new(new string[] { "1", "0", "d", "n" });
			_inputForOperationException = new(validInputSequenceExit);
		}

		#endregion

		#region Mocks

		private class MockConsoleIO : ITextIO
		{
			public string ConfirmInputLineCommandName => "Enter";
			public Queue<string> CurrentInputQueue { get; private set; }

			private readonly StringBuilder _currentOutputBuilder;
			public string CurrentOutput => _currentOutputBuilder.ToString();

			public MockConsoleIO(Queue<string> currentInputQueue)
			{
				CurrentInputQueue = currentInputQueue;
				_currentOutputBuilder = new();
			}

			public string ReadLine()
			{
				var input = CurrentInputQueue.Dequeue();
				_currentOutputBuilder.Append($"{input}\n");
				return input;
			}

			public void Write(string input)
			{
				_currentOutputBuilder.Append(input);
			}

			public void WriteLine(string input)
			{
				_currentOutputBuilder.Append($"{input}\n");
			}
		}

		private class MockCalculator : ICalculator
		{
			public double DoOperation(double number1, double number2, string operation)
			{
				throw new Exception("oops!");
			}
		}

		#endregion

		#region Tests


		[Fact]
		public void ShouldRunProgramMain()
		{
			// Given
			var mockConsoleIO = new MockConsoleIO(_inputForOneCalculationSequenceAndExit);
			Program.CalcPresenter = new(new Calculator(), mockConsoleIO);

			var expectedOutput =
				_titleOutput +
				_validInputDisplayAndEnter +
				"Your result: 3\n\n" +
				_exitConfirmedDisplay;

			// When
			Program.Main(null);

			// Then
			Assert.Equal(expectedOutput, mockConsoleIO.CurrentOutput);
		}



		[Fact]
		public void ShouldRunOneCalculationSequenceAndExit()
		{
			// Given
			var mockConsoleIO = new MockConsoleIO(_inputForOneCalculationSequenceAndExit);
			_presenter = new(new Calculator(), mockConsoleIO);
			var expectedOutput =
				_titleOutput +
				_validInputDisplayAndEnter +
				"Your result: 3\n\n" +
				_exitConfirmedDisplay;

			// When
			_presenter.RunCalculatorTextIO();

			// Then
			Assert.Equal(expectedOutput, mockConsoleIO.CurrentOutput);
		}

		[Fact]
		public void ShouldRunOneCalculationSequenceAndRestart()
		{
			// Given
			var mockConsoleIO = new MockConsoleIO(_inputForOneCalculationSequenceAndRestart);
			_presenter = new(new Calculator(), mockConsoleIO);

			var oneExpectedSequence =
				_validInputDisplayAndEnter +
				"Your result: 3\n\n";

			var expectedOutput =
				_titleOutput +
				oneExpectedSequence +
				_restartConfirmedDisplay +
				// restart
				"\n\n" +
				oneExpectedSequence +
				_exitConfirmedDisplay;

			// When
			_presenter.RunCalculatorTextIO();

			// Then
			Assert.Equal(expectedOutput, mockConsoleIO.CurrentOutput);
		}

		[Fact]
		public void ShouldSingleFailAllInput()
		{
			// Given
			var mockConsoleIO = new MockConsoleIO(_inputForSingleFailAllInput);
			_presenter = new(new Calculator(), mockConsoleIO);
			var expectedOutput =
				_titleOutput +
				"Type a number: a\n" +
				"This is not a valid input. Please enter a double or integer value: 1\n" +
				"Type another number: b\n" +
				"This is not a valid input. Please enter a double or integer value: 2\n" +
				_availableOperationsDisplay +
				"Your option? oops\n" +
				"This is not a valid input. Please enter a valid operation: a\n" +
				"Your result: 3\n\n" +
				_exitConfirmedDisplay;

			// When
			_presenter.RunCalculatorTextIO();

			// Then
			Assert.Equal(expectedOutput, mockConsoleIO.CurrentOutput);
		}

		[Fact]
		public void ShouldFailOperation()
		{
			// Given
			var mockConsoleIO = new MockConsoleIO(_inputForOperationFail);
			_presenter = new(new Calculator(), mockConsoleIO);
			var expectedOutput =
				_titleOutput +
				"Type a number: 1\n" +
				"Type another number: 0\n" +
				_availableOperationsDisplay +
				"Your option? d\n" +
				"This operation will result in a mathematical error.\n\n" +
				_exitConfirmedDisplay;

			// When
			_presenter.RunCalculatorTextIO();

			// Then
			Assert.Equal(expectedOutput, mockConsoleIO.CurrentOutput);
		}

		[Fact]
		public void ShouldFailOperationException()
		{

			// Given
			var mockConsoleIO = new MockConsoleIO(_inputForOperationException);
			_presenter = new(new MockCalculator(), mockConsoleIO);
			var expectedOutput =
				_titleOutput +
				_validInputDisplayAndEnter +
				"Oh no! An exception occurred trying to do the math.\n - Details: oops!\n\n" +
				_exitConfirmedDisplay;

			// When
			_presenter.RunCalculatorTextIO();

			// Then
			Assert.Equal(expectedOutput, mockConsoleIO.CurrentOutput);
		}

		#endregion
	}
}
