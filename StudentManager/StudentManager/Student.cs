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
		
		public int ID { get; set; }
		public string Name { get; set; }
		public List<double> Grades { get; set; }
	}
}