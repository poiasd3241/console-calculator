using System;
using System.IO;
using System.Text;
using Xunit;

namespace ConsoleCalculator.Tests
{
	public class ConsoleTextIOTests
	{
		#region Mocks

		private class MockTextReader : TextReader
		{
			public override string ReadLine()
			{
				return "read line!";
			}
		}

		private class MockTextWriter : TextWriter
		{
			public override Encoding Encoding { get; } = Encoding.ASCII;

			private readonly StringBuilder _currentOutputBuilder;
			public string CurrentOutput => _currentOutputBuilder.ToString();

			public MockTextWriter()
			{
				_currentOutputBuilder = new();
			}

			public override void WriteLine(string value)
			{
				_currentOutputBuilder.Append($"{value}\n");
			}

			public override void Write(string value)
			{
				_currentOutputBuilder.Append(value);
			}
		}

		#endregion

		#region Tests

		[Fact]
		public void ShouldCreateConsoleTextIO()
		{
			// Given
			ConsoleTextIO consoleTextIO = new();

			// When, Then
			Assert.Equal("Enter", consoleTextIO.ConfirmInputLineCommandName);
		}


		[Fact]
		public void ShouldReadLine()
		{
			// Given
			ConsoleTextIO consoleTextIO = new();
			Console.SetIn(new MockTextReader());

			// When, Then
			Assert.Equal("read line!", consoleTextIO.ReadLine());
		}

		[Fact]
		public void ShouldWriteLine()
		{
			// Given
			ConsoleTextIO consoleTextIO = new();
			MockTextWriter mockTextWriter = new();
			Console.SetOut(mockTextWriter);
			var input = "hello";

			// When
			consoleTextIO.WriteLine(input);

			// Then
			Assert.Equal(input + "\n", mockTextWriter.CurrentOutput);
		}

		[Fact]
		public void ShouldWrite()
		{
			// Given
			ConsoleTextIO consoleTextIO = new();
			MockTextWriter mockTextWriter = new();
			Console.SetOut(mockTextWriter);
			var input = "hello";

			// When
			consoleTextIO.Write(input);

			// Then
			Assert.Equal(input, mockTextWriter.CurrentOutput);
		}

		#endregion
	}
}
