namespace StudentManager
{
	class Student
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public List<double> Grades { get; set; } = new List<double>();
	}
}