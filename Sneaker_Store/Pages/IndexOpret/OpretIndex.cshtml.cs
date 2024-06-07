using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages.IndexOpret;

public class OpretIndex : PageModel
{
    
    private readonly IKundeRepository _DB_Kunde;

    public OpretIndex(IKundeRepository dbKundeRepository)
    {
        _DB_Kunde = dbKundeRepository;
    }
    [BindProperty]
    [Required(ErrorMessage = "Indtast Navn")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn skal vaere mellem 2 og 50 tegn")]
    [RegularExpression(@"^[A-Za-zÆØÅæøå ]+$", ErrorMessage = "Navn må kun indeholde bogstaver og mellemrum")]
    [Display(Name = "Navn")]
    public string navn { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Indtast Efternavn")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Efternavn skal være mellem 2 og 50 tegn")]
    [RegularExpression(@"^[A-Za-zÆØÅæøå ]+$", ErrorMessage = "Efternavn maa kun indeholde bogstaver og mellemrum")]
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
    [Required(ErrorMessage = "Indtast Postnummer")]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Postnummer skal være mellem 1000-9999")]
    [Display(Name = "Postnummer")]
    public int postnr { get; set; }


                                                                                                                                                                                                                                                            
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Kunde k = new Kunde(0, navn, efternavn, email, adresse, postnr, kode, false);
            
      _DB_Kunde.AddKunde(k);
        
        return RedirectToPage("/Login");
    }
}


