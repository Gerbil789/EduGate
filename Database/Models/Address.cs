using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Database.Models
{
    public class Address : INotifyPropertyChanged
    {
        public int AddressId { get; set; }

        private string city = string.Empty;
        public string City { get { return city; } set { SetProperty(ref city, value); } }

        private string street = string.Empty;
        public string Street { get { return street; } set { SetProperty(ref street, value); } }

        private string number = string.Empty;
        public string Number { get { return number; } set { SetProperty(ref number, value); } }

        private string zipCode = string.Empty;
        public string ZipCode { get { return zipCode; } set { SetProperty(ref zipCode, value); } }

        private string state = string.Empty;
        public string State { get { return state; } set { SetProperty(ref state, value); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public override string ToString()
        {
            return $"{Street} {Number}, {City}, {State}, {ZipCode}";
        }
    }
}
