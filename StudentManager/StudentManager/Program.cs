using GenericParse;
using CustomConsole;
namespace StudentManager
{
	class Program
	{
		// declaring _students as a pre-defined list of students
		private static List<Student> _students = new List<Student>()
		{
			new Student(1319, "Olivia Brown", new List<double>() { 72.5, 89.2, 61.8, 95.3, 76.1 }),
			new Student(15876, "Emily Johnson", new List<double>() { 63.7, 83.4, 97.0, 67.9, 74.6 }),
			new Student(299, "Noah Davis", new List<double>() { 79.2, 66.8, 91.5, 62.3, 88.7 }),
			new Student(38987, "Liam Smith", new List<double>() { 71.9, 98.6, 65.4, 82.1, 69.0 }),
			new Student(67890, "William Anderson", new List<double>() { 80.3, 94.7, 68.2, 77.8, 86.6 })
		};

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

		#region Menu
		/// <summary>
		/// Displays all menu options in the console.
		/// </summary>
		static void PrintMenu()
		{
			// saving all option arrays in an array of string arrays and calling printing them via a foreach loop.
			// if you want a unique option that doesn't really fit in a array with other options, just make it an
			// array with a single entry and add it to the local array below.
			string[][] tempArray = { StudentOptions, StudentParsingOptions, ProgramOptions };

			// printing options to console
			Console.WriteLine("Student Record Management System");
			ConsoleHelper.PrintStrings(tempArray);
			
		}

		/// <summary>
		/// Waits for user input and calls SwitchOnMenuSelection(), passing the user's input as a parameter.
		/// </summary>
		private static void SelectMenuOption()
		{
			// looping until a valid option is selected
			while (true)
			{
				ConsoleHelper.PrintBlank();
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
			ConsoleHelper.PrintBlank();

			switch (selection)
			{
				case 1: // Add new student
					AddStudent();
					break;
				case 2: // Remove student via ID
					ViewAllStudents();
					ConsoleHelper.PrintBlank();
					RemoveStudent();
					break;
				case 3: // Update student information
					ViewAllStudents();
					ConsoleHelper.PrintBlank();
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
			return tempReturnValue;
		}
		#endregion

		#region Student Functions
		/// <summary>
		/// Finds if a student exists in the list of students based on the student's ID.
		/// </summary>
		/// <param name="id">Unique student ID</param>
		/// <returns>Does student exist</returns>
		public static bool StudentExists(int id)
		{
			return _students.Exists(s => s.ID == id);
		}

		/// <summary>
		/// Prompts the user to enter a student ID
		/// </summary>
		/// <param name="preventDupe">Should check if student already exists</param>
		/// <param name="flavorText">Console text prompting user input</param>
		/// <returns>A new student ID</returns>
		static int MakeStudentId(bool preventDupe = true, string flavorText = "Enter student ID: ")
		{
			int tempID;

			Console.Write(flavorText);
			if (preventDupe)
			{
				while (true)
				{
					tempID = GenericReadLine.TryReadLine<int>();
					if (StudentExists(tempID))
					{
						Console.WriteLine("Student with this ID already exists.");
					}
					else
					{
						break;
					}
				}
			}
			else
			{
				tempID = GenericReadLine.TryReadLine<int>();
			}
			return tempID;
		}

		/// <summary>
		/// Prompts the user to enter a student name
		/// </summary>
		/// <param name="flavorText">Console text prompting user input</param>
		/// <returns>A new student name</returns>
		static string MakeStudentName(string flavorText = "Enter student name: ")
		{
			// if the user doesn't enter a name, the name will be set to "Unknown"
			Console.Write(flavorText);
			return Console.ReadLine() ?? "Unknown";
		}

		/// <summary>
		/// Prompts the user to enter a student's grades
		/// </summary>
		/// <param name="flavorText">Console text prompting user input</param>
		/// <returns>A list of grades</returns>
		static List<double> MakeStudentGrades(string flavorText = "Enter grades (separated by spaces): ")
		{
			Console.Write(flavorText);
			return ConvertGrades(Console.ReadLine().Split(' '));
		}

		/// <summary>
		/// Converts an array of strings to a list of doubles.
		/// </summary>
		/// <param name="grades">Student grades in the format of a string array</param>
		/// <returns>Student grades in the format of a List</returns>
		private static List<double> ConvertGrades(string[] grades)
		{
			List<double> tempGrades = new List<double>();

			foreach (string grade in grades)
			{
				if (double.TryParse(grade, out double tempGrade))
				{
					tempGrades.Add(tempGrade);
				}
				else
				{
					Console.WriteLine($"Invalid grade: {grade}");
				}
			}
			return tempGrades;
		}
		#endregion

		#region Student Management
		/// <summary>
		/// Adds new student to the list of students with a unique ID, name and grades.
		/// </summary>
		static void AddStudent()
		{
			// constructing student object and adding it to the list of students
			Student tempStudent = new Student(MakeStudentId(), MakeStudentName(), MakeStudentGrades());
			
			_students.Add(tempStudent);
			
			Console.WriteLine("Student added successfully.");
		}

		/// <summary>
		/// Removes a student from the list of students using their ID.
		/// </summary>
		static void RemoveStudent()
		{
			int id = MakeStudentId(false);

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
			int id = MakeStudentId(false);

			// finding student with the specified ID
			Student studentToUpdate = _students.Find(s => s.ID == id);
			if (studentToUpdate != null)
			{
				studentToUpdate.Name = MakeStudentName();

				studentToUpdate.Grades.Clear();
				studentToUpdate.Grades = MakeStudentGrades();

				Console.WriteLine($"Student {id} updated successfully.");
			}
			else
			{
				PrintNoStudentsFound();
			}
		}

		/// <summary>
		/// Prints all students in the list of students.
		/// </summary>
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

		/// <summary>
		/// Finds the student with the highest average grade and prints their ID, name and average grade.
		/// </summary>
		static void FindHighestAverageGrade()
		{
			double highestAverage = 0;
			Student studentWithHighestAverage = null;

			foreach (Student student in _students)
			{
				double average = student.AverageGrade;
				if (average > highestAverage)
				{
					highestAverage = average;
					studentWithHighestAverage = student;
				}
			}

			if (studentWithHighestAverage != null)
			{
				Console.WriteLine("Student with highest average grade: ");
				Console.WriteLine($"ID: {studentWithHighestAverage.ID}, Name: {studentWithHighestAverage.Name}, Average Grade: {highestAverage}");
			}
			else
			{
				PrintNoStudentsFound();
			}
		}
		#endregion

		public static void PrintNoStudentsFound()
		{
			Console.WriteLine("No student(s) found.");
		}
	}
}