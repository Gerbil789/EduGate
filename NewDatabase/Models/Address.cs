using NewDatabase.Attributes;

namespace NewDatabase.Models
{
    [DbTable("Address")]
    public class Address
    {
        [PrimaryKey]
        [AutoIncrement]
        public int AddressId { get; set; }


        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Street} {Number}, {City}, {State}, {ZipCode}";
        }
    }
}
