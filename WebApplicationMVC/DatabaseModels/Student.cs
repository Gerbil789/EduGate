using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplicationMVC.DatabaseModels
{
    public class Student
    {
        public int StudentId { get; set; }


        [Display(Name = "Jméno")]
        [Required(ErrorMessage = "Jméno je povinné")]
        [StringLength(50, ErrorMessage = "Max. počet znaků je 50")]
        public string FirstName { get; set; } = string.Empty;


        [Display(Name = "Příjmení")]
        [Required(ErrorMessage = "Příjmení je povinné")]
        [StringLength(50, ErrorMessage = "Max. počet znaků je 50")]
        public string LastName { get; set; } = string.Empty;


        [Display(Name = "Datum narození")]
        [Required(ErrorMessage = "Datum narození je povinné")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [Display(Name = "Adresa")]
        public Address Address { get; set; }

     

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email je povinný.")]
        public string Email { get; set; }


        [DisplayName("Předčíslí")]
        [Required(ErrorMessage = "Předčíslí je povinné")]
        [RegularExpression(@"^\+?\d{3}$", ErrorMessage = "Předčíslí musí mít 3 číslice")]
        public string Code { get; set; } = "+420";


        [DisplayName("Telefonní číslo")]
        [Required(ErrorMessage = "Telefonní číslo je povinné")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Telefonní číslo musí mít 9 číslic (bez mezer)")]
        public string Number { get; set; } = string.Empty;


        public List<Application> Applications { get; set; }


        [Display(Name = "Heslo")]
        [Required(ErrorMessage = "Heslo je povinné")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Heslo musí mít 8 až 32 znaků")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$", ErrorMessage = "Heslo musí obsahovat alespoň jedno velké písmeno, jedno malé písmeno, jedno číslo a jedno speciální znak")]
        public string Password { get; set; } 


        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
