namespace StudentManager
{
	public class Student
	{
		public Student(int id, string name)
		{
			ID = id;
			Name = name;
		}
		
		public int ID { get; set; }
		public string Name { get; set; }
		public List<double> Grades { get; set; } = new List<double>();
	}
}