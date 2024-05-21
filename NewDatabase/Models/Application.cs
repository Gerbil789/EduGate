using NewDatabase.Attributes;

namespace NewDatabase.Models
{
    [DbTable("Application")]
    public class Application
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ApplicationId { get; set; }

        [ForeignKey("Student", "StudentId")]
        public int StudentId { get; set; }

        [ForeignKey("StudyProgram", "StudyProgramId")]
        public int StudyProgramId { get; set; }

        public DateTime Date { get; set; }


        public override string ToString()
        {
            return $"Application {ApplicationId}";
        }
    }
}
