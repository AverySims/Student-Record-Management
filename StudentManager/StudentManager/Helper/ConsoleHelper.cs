using System;
using GenericParse;

namespace ConsoleFunctions
{
	static class ConsoleHelper
	{
		public static void SelectEndingAction(out bool mainLoop)
		{
			Console.WriteLine("Choose what happens next:");
			Console.WriteLine("1. Restart program");
			Console.WriteLine("2. Quit program");

			bool loopValid = false;
			bool tempLoopValue = false;
			do
			{
				int tempSelect = GenericReadLine.TryReadLine<int>();
				switch (tempSelect)
				{
					case 1:
						loopValid = !loopValid;
						tempLoopValue = true;
						Console.Clear(); // clear screen to make room for new info
						break;

					case 2:
						loopValid = !loopValid;
						tempLoopValue = false;
						break;

					default:
						PrintInvalidSelection();
						break;
				}
			} while (!loopValid);

			// "The Out Parameter must be assigned before control leaves the current method"
			// So we just use a temp value and assign it
			// to the actual value once the switch is over
			mainLoop = tempLoopValue;

		}

		#region Parsing
		public static ConsoleKeyInfo UserEndProgram()
		{
			Console.WriteLine("Input any key to close program...");
			return Console.ReadKey();
		}
		#endregion

		#region Printing
		public static void PrintBlank()
		{
			Console.WriteLine();
		}

		public static void PrintValue<T>(T val)
		{
			Console.WriteLine($"Value is: {val}");
		}

		public static void PrintInvalidSelection()
		{
			Console.WriteLine("Invalid selection, please select a listed option.");
		}
		#endregion
	}
}
