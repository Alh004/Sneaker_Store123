using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;

namespace Sneaker_Store.IndexOpret;

public class OpretIndex : PageModel
{
    // ...
    [BindProperty]
    [Required(ErrorMessage = "Indtast Navn")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn skal være mellem 2 og 50 tegn")]
    [RegularExpression(@"^[A-Za-zÆØÅæøå ]+$", ErrorMessage = "Navn må kun indeholde bogstaver og mellemrum")]
    [Display(Name = "Navn")]
    public string navn { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Indtast Efternavn")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Efternavn skal være mellem 2 og 50 tegn")]
    [RegularExpression(@"^[A-Za-zÆØÅæøå ]+$", ErrorMessage = "Efternavn må kun indeholde bogstaver og mellemrum")]
    [Display(Name = "Efternavn")]
    public string efternavn { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Indtast Adresse")]
    [Display(Name = "Adresse")]
    public string adresse { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Indtast Email")]
    [EmailAddress(ErrorMessage = "Indtast venligst en gyldig email-adresse")]
    [Display(Name = "Email")]
    public string email { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Indtast Adgangskode")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "Adgangskode skal være 8 tegn")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8}$",
        ErrorMessage = "Adgangskode skal indeholde mindst ét bogstav og ét tal")]
    [Display(Name = "Adgangskode")]
    public string kode { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Indtast By")]
    [Display(Name = "By")]
    public string by { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Indtast Postnummer")]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Postnummer skal være 4 cifre")]
    [Display(Name = "Postnummer")]
    public int postnr { get; set; }

    // ...

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

      
        
        return RedirectToPage("/Privacy");
    }
}


