using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Database.Models
{
    public class Student : INotifyPropertyChanged
    {
        public int StudentId{ get; set; }

        private string firstName = string.Empty;
        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        private string lastName = string.Empty;
        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        private DateTime birthDate = new DateTime(2000, 1, 1);
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { SetProperty(ref birthDate, value); }
        }

        private Address address = new();
        public Address Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        private string email = string.Empty;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private Phone phone = new();
        public Phone Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }

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
            return $"{FirstName} {LastName}";
        }
    }
}
