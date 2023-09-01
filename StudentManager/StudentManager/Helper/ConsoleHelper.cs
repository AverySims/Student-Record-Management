using System;
using GenericParse;

namespace CustomConsole
{
	static class ConsoleHelper
	{
		public static void SelectEndingAction(out bool mainLoop)
		{
			// reset loop state before entering loop
			bool tempLoopValue = false;
			bool loopEndingSelector = true;

			Console.WriteLine("Choose what happens next:");
			PrintBlank();
			Console.WriteLine("1. Restart program");
			Console.WriteLine("2. Quit program");

			while (loopEndingSelector)
			{
				int userSelection = GenericReadLine.TryReadLine<int>();
				switch (userSelection)
				{
					case 1:
						loopEndingSelector = false;
						tempLoopValue = true;
						Console.Clear(); // clear screen to make room for new info
						break;

					case 2:
						loopEndingSelector = false;
						tempLoopValue = false;
						PrintBlank(); // write buffer line to keep results on-screen after program ends
						break;

					default:
						tempLoopValue = true;
						PrintInvalidSelection();
						break;
				}
			}
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

		#region Menu Printing
		public static void PrintStrings(string[] strings)
		{
			for (int i = 0; i < strings.Length; i++)
			{
				Console.WriteLine($"{i + 1}. {strings[i]}");
			}
		}

		public static void PrintStrings(string[][] strings)
		{
			int tempIndex = 0;
			foreach (var menu in strings)
			{
				for (int i = 0; i < menu.Length; i++)
				{
					tempIndex++;
					Console.WriteLine($"{tempIndex}. {menu[i]}");
				}
			}
		}
		#endregion

		#region Basic Printing
		public static void PrintBlank()
		{
			Console.WriteLine();
		}

		public static void PrintValue<T>(T val)
		{
			Console.WriteLine($"{nameof(val)} is: {val}");
		}

		public static void PrintInvalidSelection()
		{
			Console.WriteLine("Invalid entry, please try again.");
		}
		#endregion
		
		public static bool ListEmpty<T>(List<T> list)
		{
			return !(list.Count > 0);
		}
	}
}
