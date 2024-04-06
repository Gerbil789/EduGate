
namespace Database.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public Student Student { get; set; } = new();
        public List<StudyProgram> StudyPrograms { get; set; } = new();
        public bool IsAccepted { get; set; } = false;
    }
}
