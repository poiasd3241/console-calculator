namespace ConsoleCalculator
{
	public interface ITextIO
	{
		/// <summary>
		/// The name of the command or the key that confirms variable-length input.
		/// </summary>
		string ConfirmInputLineCommandName { get; }
		string ReadLine();
		void WriteLine(string input);
		void Write(string input);
	}
}
