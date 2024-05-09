using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Database.Models
{
    public class Address : INotifyPropertyChanged
    {
        public int AddressId { get; set; }

        private string city = string.Empty;
        [Display(Name = "Město")]
        [Required(ErrorMessage = "Město je povinné. Prosím, zadejte město.")]
        [RegularExpression(@"^[a-zA-Zá-žÁ-Ž\s]*$", ErrorMessage = "Město může obsahovat pouze písmena a mezery.")]
        public string City { get { return city; } set { SetProperty(ref city, value); } }

        private string street = string.Empty;
        [Display(Name = "Ulice")]
        [Required(ErrorMessage = "Ulice je povinná. Prosím, zadejte ulici.")]
        [RegularExpression(@"^[a-zA-Zá-žÁ-Ž\s]*$", ErrorMessage = "Ulice může obsahovat pouze písmena a mezery.")]
        public string Street { get { return street; } set { SetProperty(ref street, value); } }

        private string number = string.Empty;
        [Display(Name = "Číslo popisné")]
        [Required(ErrorMessage = "Číslo popisné je povinné. Prosím, zadejte číslo popisné.")]
        [RegularExpression(@"^\d+[a-zA-Z]*$", ErrorMessage = "Zadejte prosím číslo popisné ve formátu XXXX nebo XXXXa")]
        public string Number { get { return number; } set { SetProperty(ref number, value); } }

        private string zipCode = string.Empty;
        [Display(Name = "PSČ")]
        [Required(ErrorMessage = "PSČ je povinné. Prosím, zadejte PSČ.")]
        [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "Zadejte prosím PSČ ve formátu XXXXX")]
        public string ZipCode { get { return zipCode; } set { SetProperty(ref zipCode, value); } }

        private string state = string.Empty;
        [Display(Name = "Stát")]
        [Required(ErrorMessage = "Stát je povinný. Prosím, zadejte stát.")]
        [RegularExpression(@"^[a-zA-Zá-žÁ-Ž\s]*$", ErrorMessage = "Stát může obsahovat pouze písmena a mezery.")]
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
