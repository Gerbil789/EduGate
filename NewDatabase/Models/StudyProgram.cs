using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using NewDatabase.Attributes;

namespace NewDatabase.Models
{
    [DbTable("StudyProgram")]
    public class StudyProgram
    {
        [PrimaryKey]
        [AutoIncrement]
        public int StudyProgramId { get; set; }

        [ForeignKey("School", "SchoolId")]
        public int SchoolId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AvailableSeats { get; set; } 



        public override string ToString()
        {
            return $"{Name}-{AvailableSeats}";
        }
    }
}
