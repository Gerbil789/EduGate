using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class StudyProgram
    {
        public int StudyProgramId { get; set; }
        public School School { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public int AvailableSeats { get; set; }
    }
}
