using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WebApplicationMVC.DatabaseModels
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public DateTime Date { get; set; }

        public StudyProgram? StudyProgram { get; set; }

        public Student Student { get; set; }

       

        public override string ToString()
        {
            return $"{Student} - {StudyProgram}";
        }
    }
}
