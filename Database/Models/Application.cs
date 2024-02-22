using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public Student Student { get; set; }
        public List<StudyProgram> StudyPrograms { get; set; }
    }
}
