
namespace Database.Models
{
    public class School
    {
        public School() { }
        public School(School other)
        {
            SchoolId = other.SchoolId;
            Name = other.Name;
            Address = new Address(other.Address);
        }
        public int SchoolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Address Address { get; set; } = new();
    }
}
