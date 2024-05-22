

namespace WebApplicationMVC.DatabaseModels
{
    public class StudyProgram
    {
        public int StudyProgramId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AvailableSeats { get; set; } 

        public School School { get; set; }


        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
