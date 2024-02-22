namespace Database.Models
{
    public class School
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class StudyProgram
    {
        public int StudyProgramId { get; set; }
        public School School { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public int AvailableSeats { get; set; }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

    }

    public class Application
    {
        public int ApplicationId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public Student Student { get; set; }
        public List<StudyProgram> StudyPrograms { get; set; }
    }

}
