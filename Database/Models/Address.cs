using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Database.Models
{
    public class Address : INotifyPropertyChanged
    {
        public Address() { }
        public Address(Address other)
        {
            City = other.City;
            Street = other.Street;
            Number = other.Number;
            ZipCode = other.ZipCode;
            State = other.State;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Notify<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int AddressId { get; set; }

        private string city = string.Empty;
        public string City { get { return city; } set { Notify(ref city, value); } }

        private string street = string.Empty;
        public string Street { get { return street; } set { Notify(ref street, value); } }

        private string number = string.Empty;
        public string Number { get { return number; } set { Notify(ref number, value); } }

        private string zipCode = string.Empty;
        public string ZipCode { get { return zipCode; } set { Notify(ref zipCode, value); } }

        private string state = string.Empty;
        public string State { get { return state; } set { Notify(ref state, value); } }

        public override string ToString()
        {
            return $"{Street} {Number}, {City}, {State}, {ZipCode}";
        }
    }
}
