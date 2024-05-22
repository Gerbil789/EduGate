using System.Collections.ObjectModel;

namespace WebApplicationMVC.DatabaseModels
{
    public class School
    {
        public int SchoolId { get; set; }


        public string Name { get; set; } = string.Empty;

        public Address Address { get; set; } = new();

        public List<StudyProgram> StudyPrograms { get; set; } = new();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
