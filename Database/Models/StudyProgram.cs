
namespace Database.Models
{
    public class StudyProgram
    {
        public int StudyProgramId { get; set; }
        public School School { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AvailableSeats { get; set; }
    }
}
