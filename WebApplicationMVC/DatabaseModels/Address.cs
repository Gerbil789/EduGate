using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.DatabaseModels
{
    public class Address
    {
        public int AddressId { get; set; }

        [Display(Name = "Město")]
        [Required(ErrorMessage = "Město je povinné. Prosím, zadejte město.")]
        [RegularExpression(@"^[a-zA-Zá-žÁ-Ž\s]*$", ErrorMessage = "Město může obsahovat pouze písmena a mezery.")]
        public string City { get; set; } = string.Empty;


        [Display(Name = "Ulice")]
        [Required(ErrorMessage = "Ulice je povinná. Prosím, zadejte ulici.")]
        [RegularExpression(@"^[a-zA-Zá-žÁ-Ž\s]*$", ErrorMessage = "Ulice může obsahovat pouze písmena a mezery.")]
        public string Street { get; set; } = string.Empty;


        [Display(Name = "Číslo popisné")]
        [Required(ErrorMessage = "Číslo popisné je povinné. Prosím, zadejte číslo popisné.")]
        [RegularExpression(@"^\d+[a-zA-Z]*$", ErrorMessage = "Zadejte prosím číslo popisné ve formátu XXXX nebo XXXXa")]
        public string Number { get; set; } = string.Empty;


        [Display(Name = "PSČ")]
        [Required(ErrorMessage = "PSČ je povinné. Prosím, zadejte PSČ.")]
        [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "Zadejte prosím PSČ ve formátu XXXXX")]
        public string ZipCode { get; set; } = string.Empty;


        [Display(Name = "Stát")]
        [Required(ErrorMessage = "Stát je povinný. Prosím, zadejte stát.")]
        [RegularExpression(@"^[a-zA-Zá-žÁ-Ž\s]*$", ErrorMessage = "Stát může obsahovat pouze písmena a mezery.")]
        public string State { get; set; } = string.Empty;

      

        public override string ToString()
        {
            return $"{Street} {Number}, {City}, {State}, {ZipCode}";
        }
    }
}
