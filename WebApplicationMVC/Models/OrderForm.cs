using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
  public class OrderForm
  {
    [Display(Name = "Jméno")]
    public string? Name { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "bruh")]
    public string? Email { get; set; }

    [Display(Name = "Počet")]
    [Required(ErrorMessage = "bruh")]
    [Range(1, 10)]
    public int? Quantity { get; set; }
  }
}
