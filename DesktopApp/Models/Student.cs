using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DesktopApp.Models
{
    public class Student : INotifyPropertyChanged
    {
        public int StudentId { get; set; }

        private string firstName = string.Empty;
        public string FirstName { get { return firstName; } set { SetProperty(ref firstName, value); } }

        private string lastName = string.Empty;
        public string LastName { get { return lastName; } set { SetProperty(ref lastName, value); } }

        private DateTime birthDate;
        public DateTime BirthDate { get { return birthDate; } set { SetProperty(ref birthDate, value); } }

        private Address address = new();
        public Address Address { get { return address; } set { SetProperty(ref address, value); } }

        private string email = string.Empty;
        public string Email { get { return email; } set { SetProperty(ref email, value); } }

        private string code = string.Empty;
        public string Code { get { return code; } set { SetProperty(ref code, value); } }

        private string number = string.Empty;
        public string Number { get { return number; } set { SetProperty(ref number, value); } }



        private List<Application> applications = new();
        public List<Application> Applications { get { return applications; } set { SetProperty(ref applications, value); OnPropertyChanged(nameof(ApplicationCount)); } }
        public int ApplicationCount { get => applications.Where(x => x.StudyProgram != null).Count(); }



        private string password = string.Empty;
        public string Password { get { return password; } set { SetProperty(ref password, value); } }


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
