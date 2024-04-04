using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Database.Models
{
    public class Student : INotifyPropertyChanged
    {
        public Student() 
        { 
            address = new Address();
        }
        public Student(Student other)
        {
            StudentId = other.StudentId;
            FirstName = other.FirstName;
            LastName = other.LastName;
            BirthDate = other.BirthDate;
            Address = new (other.Address); 
            Email = other.Email;
            Phone = other.Phone;
        }

        public void Update(Student other)
        {
            StudentId = other.StudentId;
            FirstName = other.FirstName;
            LastName = other.LastName;
            BirthDate = other.BirthDate;
            Address = new(other.Address);
            Email = other.Email;
            Phone = other.Phone;
        }

        public int StudentId{ get; set; }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { Notify(ref firstName, value); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { Notify(ref lastName, value); }
        }

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { Notify(ref birthDate, value); }
        }

        private Address address;
        public Address Address
        {
            get { return address; }
            set { Notify(ref address, value); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { Notify(ref email, value); }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { Notify(ref phone, value); }
        }
        

        private void Notify<T>(ref T prop, T val, [CallerMemberName] string name = null)
        {
            prop = val;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
