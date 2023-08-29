namespace StudentManager
{
	class Program
	{
		static List<Student> students = new List<Student>();

		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("Student Record Management System");
				Console.WriteLine("1. Add a student");
				Console.WriteLine("2. Remove a student by ID");
				Console.WriteLine("3. Update student information");
				Console.WriteLine("4. View all students");
				Console.WriteLine("5. Find student with the highest average grade");
				Console.WriteLine("6. Exit");
				Console.Write("Select an option: ");

				int choice = int.Parse(Console.ReadLine());

				switch (choice)
				{
					case 1:
						AddStudent();
						break;
					case 2:
						RemoveStudent();
						break;
					case 3:
						UpdateStudent();
						break;
					case 4:
						ViewAllStudents();
						break;
					case 5:
						FindHighestAverageGrade();
						break;
					case 6:
						Console.WriteLine("Exiting...");
						return;
					default:
						Console.WriteLine("Invalid choice. Please select a valid option.");
						break;
				}
			}
		}

		static void AddStudent()
		{
			Student student = new Student();

			Console.Write("Enter student ID: ");
			int id = int.Parse(Console.ReadLine());

			if (StudentExists(id))
			{
				Console.WriteLine("Student with this ID already exists.");
				return;
			}

			student.ID = id;

			Console.Write("Enter student name: ");
			student.Name = Console.ReadLine();

			Console.Write("Enter grades (separated by spaces): ");
			string[] gradeStrings = Console.ReadLine().Split(' ');
			foreach (string gradeString in gradeStrings)
			{
				if (double.TryParse(gradeString, out double grade))
				{
					student.Grades.Add(grade);
				}
			}

			students.Add(student);
			Console.WriteLine("Student added successfully.");
		}

		static void RemoveStudent()
		{
			Console.Write("Enter student ID to remove: ");
			int id = int.Parse(Console.ReadLine());

			Student studentToRemove = students.Find(s => s.ID == id);
			if (studentToRemove != null)
			{
				students.Remove(studentToRemove);
				Console.WriteLine("Student removed successfully.");
			}
			else
			{
				Console.WriteLine("Student not found.");
			}
		}

		static void UpdateStudent()
		{
			Console.Write("Enter student ID to update: ");
			int id = int.Parse(Console.ReadLine());

			Student studentToUpdate = students.Find(s => s.ID == id);
			if (studentToUpdate != null)
			{
				Console.Write("Enter new student name: ");
				studentToUpdate.Name = Console.ReadLine();

				Console.Write("Enter new grades (separated by spaces): ");
				string[] gradeStrings = Console.ReadLine().Split(' ');
				studentToUpdate.Grades.Clear();
				foreach (string gradeString in gradeStrings)
				{
					if (double.TryParse(gradeString, out double grade))
					{
						studentToUpdate.Grades.Add(grade);
					}
				}

				Console.WriteLine("Student information updated successfully.");
			}
			else
			{
				Console.WriteLine("Student not found.");
			}
		}

		static void ViewAllStudents()
		{
			foreach (Student student in students)
			{
				Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Grades: {string.Join(", ", student.Grades)}");
			}
		}

		static void FindHighestAverageGrade()
		{
			double highestAverage = 0;
			Student studentWithHighestAverage = null;

			foreach (Student student in students)
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
				Console.WriteLine("No students found.");
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

		static bool StudentExists(int id)
		{
			return students.Exists(s => s.ID == id);
		}
	}
}