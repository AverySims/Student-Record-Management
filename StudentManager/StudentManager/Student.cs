namespace StudentManager
{
	public class Student
	{
		public Student(int id, string name, List<double> grades)
		{
			ID = id;
			Name = name;
			Grades = grades;
		}
		
		public double AverageGrade
		{
			// If there are no grades, return 0
			// Otherwise, return the average of the grades
			get { return Grades.Count == 0 ? 0 : Grades.Average(); }
		}
		
		public int ID { get; set; }
		public string Name { get; set; }
		public List<double> Grades { get; set; }
	}
}