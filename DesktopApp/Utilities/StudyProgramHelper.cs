using NewDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Utilities
{
    public static class StudyProgramHelper
    {
        public static string GenerateProgramIdentifier(StudyProgram program)
        {
            return$"{program.Name.Substring(0, 3).ToUpper()}-{program.AvailableSeats}";
        }
    }
}
