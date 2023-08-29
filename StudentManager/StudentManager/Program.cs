using System.Diagnostics;
using ConsoleFunctions;
using GenericParse;

namespace StudentManager
{
	class Program
	{
		private static List<Student> _students = new List<Student>();

		// splitting menu options into separate string arrays for "easier" sorting
		private static readonly string[] StudentOptions =
			{ "Add new student", "Remove student via ID", "Update student information" };

		private static readonly string[] StudentParsingOptions =
			{ "View all students", "Find student with highest average grade" };

		private static readonly string[] ProgramOptions = { "Exit program" };

		private static bool _loopMain = true;

		static void Main(string[] args)
		{
			while (_loopMain)
			{
				PrintMenu();

				SelectMenuOption();
			}
		}

		/// <summary>
		/// Displays all menu options in the console.
		/// </summary>
		static void PrintMenu()
		{
			// saving all option arrays in an array of string arrays and calling printing them via a foreach loop.
			// if you want a unique option that doesn't really fit in a array with other options, just make it an
			// array with a single entry and add it to the local array below.
			string[][] tempArray = { StudentOptions, StudentParsingOptions, ProgramOptions };
			int tempIndex = 0;

			// printing options to console
			Console.WriteLine("Student Record Management System");
			foreach (var option in tempArray)
			{
				for (int i = 0; i < option.Length; i++)
				{
					Console.WriteLine($"{tempIndex + 1}. {option[i]}");
					tempIndex++;
				}
			}
			ConsoleHelper.PrintBlank();
		}

		/// <summary>
		/// Waits for user input and calls SwitchOnMenuSelection(), passing the user's input as a parameter.
		/// </summary>
		private static void SelectMenuOption()
		{
			// looping until a valid option is selected
			while (true)
			{
				Console.Write("Select option: ");
				int tempSelect = GenericReadLine.TryReadLine<int>();

				if (!SwitchOnMenuSelection(tempSelect))
				{
					break;
				}
			}
		}

		/// <summary>
		/// Uses a switch statement to call the appropriate method based on the user's menu selection.
		/// </summary>
		/// <param name="selection">The user's menu selection</param>
		/// <returns>The desired loop state</returns>
		private static bool SwitchOnMenuSelection(int selection)
		{
			bool tempReturnValue = true;

			// clearing console and printing menu again to prevent clutter
			Console.Clear();
			PrintMenu();

			switch (selection)
			{
				case 1: // Add new student
					AddStudent();
					break;
				case 2: // Remove student via ID
					RemoveStudent();
					break;
				case 3: // Update student information
					UpdateStudent();
					break;
				case 4: // View all students
					ViewAllStudents();
					break;
				case 5: // Find student with highest average grade
					FindHighestAverageGrade();
					break;
				case 6: // Exit program
					tempReturnValue = false;
					_loopMain = false;
					Console.WriteLine("Exiting program.");
					break;
				default: // Invalid selection
					ConsoleHelper.PrintInvalidSelection();
					break;
			}
			ConsoleHelper.PrintBlank();
			return tempReturnValue;
		}

		/// <summary>
		/// Adds new student to the list of students with a unique ID, name and grades.
		/// </summary>
		static void AddStudent()
		{
			// declaring variables
			string name = " ";
			int id = 0;
			string[] grades;

			// Loop until we get a unique ID
			while (true)
			{
				Console.Write("Enter student ID: ");
				id = GenericReadLine.TryReadLine<int>();
				
				if (StudentExists(id))
				{
					Console.WriteLine("Student with this ID already exists.");
				}
				else
				{
					break;
				}
			}
			
			// if the user enters nothing, we set the name to a space to prevent null exceptions
			Console.Write("Enter student name: ");
			name = Console.ReadLine() ?? " ";

			Console.Write("Enter grades (separated by spaces): ");
			grades = Console.ReadLine().Split(' ');
			
			// constructing student object and adding it to the list of students
			Student student = new Student(id, name);
			AddGrades(grades, student);
			_students.Add(student);
			
			// clearing console and printing menu again to prevent clutter
			Console.Clear();
			PrintMenu();

			// printing blank line to separate output
			Console.WriteLine("Student added successfully.");
			
		}

		/// <summary>
		/// Removes a student from the list of students using their ID.
		/// </summary>
		static void RemoveStudent()
		{
			Console.Write("Enter student ID to remove: ");
			int id = GenericReadLine.TryReadLine<int>();

			Student studentToRemove = _students.Find(s => s.ID == id);

			if (studentToRemove != null)
			{
				_students.Remove(studentToRemove);
				Console.WriteLine("Student removed successfully.");
			}
			else
			{
				PrintNoStudentsFound();
			}
		}

		/// <summary>
		/// Takes a student ID and allows the user to update the student's name and grades.
		/// </summary>
		static void UpdateStudent()
		{
			Console.Write("Enter student ID to update: ");
			int id = GenericReadLine.TryReadLine<int>();

			Student studentToUpdate = _students.Find(s => s.ID == id);

			if (studentToUpdate != null)
			{
				Console.Write("Enter new student name: ");
				studentToUpdate.Name = Console.ReadLine();

				Console.Write("Enter new grades (separated by spaces): ");
				string[] gradeStrings = Console.ReadLine().Split(' ');

				studentToUpdate.Grades.Clear();

				AddGrades(gradeStrings, studentToUpdate);

				Console.WriteLine("Student information updated successfully.");
			}
			else
			{
				PrintNoStudentsFound();
			}
		}

		private static void AddGrades(string[] grades, Student student)
		{
			foreach (string grade in grades)
			{
				if (double.TryParse(grade, out double tempGrade))
				{
					student.Grades.Add(tempGrade);
				}
				else
				{
					Console.WriteLine($"Invalid grade: {grade}");
				}
			}
		}

		static void ViewAllStudents()
		{
			if (_students.Count <= 0)
			{
				PrintNoStudentsFound();
			}
			else
			{
				foreach (Student student in _students)
				{
					Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Grades: {string.Join(", ", student.Grades)}");
				}
			}
		}

		static void FindHighestAverageGrade()
		{
			double highestAverage = 0;
			Student studentWithHighestAverage = null;

			foreach (Student student in _students)
			{
				double average = CalculateAverage(student.Grades);
				if (average > highestAverage)
				{
					highestAverage = average;
					studentWithHighestAverage = student;
				}
			}

			if (studentWithHighestAverage != null)
			{
				Console.WriteLine($"Student with highest average grade: ID: {studentWithHighestAverage.ID}, Name: {studentWithHighestAverage.Name}, Average Grade: {highestAverage}");
			}
			else
			{
				PrintNoStudentsFound();
			}
		}

		static double CalculateAverage(List<double> grades)
		{
			if (grades.Count == 0)
			{
				return 0;
			}
			double sum = 0;
			foreach (double grade in grades)
			{
				sum += grade;
			}
			return sum / grades.Count;
		}

		public static bool StudentExists(int id)
		{
			return _students.Exists(s => s.ID == id);
		}

		public static void PrintNoStudentsFound()
		{
			Console.WriteLine("No student(s) found.");
		}


	}
}