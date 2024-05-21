using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NewDatabase.Attributes;

namespace NewDatabase.Models
{
    [DbTable("School")]
    public class School 
    {
        [PrimaryKey]
        [AutoIncrement]
        public int SchoolId { get; set; }

        [ForeignKey("Address", "AddressId")]
        public int AddressId { get; set; }

        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
