using Database.Validation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Database.Models
{
    public class Student : INotifyPropertyChanged
    {
        public int StudentId { get; set; }

        private string firstName = string.Empty;
        [Display(Name = "Jméno")]
        [Required(ErrorMessage = "Jméno je povinné")]
        [StringLength(50, ErrorMessage = "Max. počet znaků je 50")]
        public string FirstName { get { return firstName; } set { SetProperty(ref firstName, value); } }

        private string lastName = string.Empty;
        [Display(Name = "Příjmení")]
        [Required(ErrorMessage = "Příjmení je povinné")]
        [StringLength(50, ErrorMessage = "Max. počet znaků je 50")]
        public string LastName { get { return lastName; } set { SetProperty(ref lastName, value); } }

        private DateTime birthDate;
        [Display(Name = "Datum narození")]
        [Required(ErrorMessage = "Datum narození je povinné")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get { return birthDate; } set { SetProperty(ref birthDate, value); } }

        private Address address = new();
        [Display(Name = "Adresa")]
        public Address Address { get { return address; } set { SetProperty(ref address, value); } }

        private string email = string.Empty;
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email je povinný.")]
        [Email]
        public string Email { get { return email; } set { SetProperty(ref email, value); } }

        private Phone phone = new();
        public Phone Phone { get { return phone; } set { SetProperty(ref phone, value); } }

        private Application application1 = new();
        public Application Application1 { get { return application1; } set { SetProperty(ref application1, value); OnPropertyChanged(nameof(ApplicationCount)); } }

        private Application application2 = new();
        public Application Application2 { get { return application2; } set { SetProperty(ref application2, value); OnPropertyChanged(nameof(ApplicationCount)); } }

        private Application application3 = new();
        public Application Application3 { get { return application3; } set { SetProperty(ref application3, value); OnPropertyChanged(nameof(ApplicationCount)); } }
        public int ApplicationCount {  get =>(Application1.StudyProgram != null ? 1 : 0) + (Application2.StudyProgram != null ? 1 : 0) + (Application3.StudyProgram != null ? 1 : 0); }
            
            
        private string password = string.Empty;
        [Display(Name = "Heslo")]
        [Required(ErrorMessage = "Heslo je povinné")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Heslo musí mít 8 až 32 znaků")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$", ErrorMessage = "Heslo musí obsahovat alespoň jedno velké písmeno, jedno malé písmeno, jedno číslo a jedno speciální znak")]
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
