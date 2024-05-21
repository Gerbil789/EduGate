using NewDatabase.Attributes;   

namespace NewDatabase.Models
{
    [DbTable("Student")]
    public class Student
    {
        [PrimaryKey]
        [AutoIncrement]
        public int StudentId { get; set; }

        [ForeignKey("Address", "AddressId")]
        public int AddressId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = "+420";
        public string Number { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
