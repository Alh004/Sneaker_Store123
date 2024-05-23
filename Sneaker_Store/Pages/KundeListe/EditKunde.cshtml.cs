using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using System.ComponentModel.DataAnnotations;

namespace Sneaker_Store.Pages.KundeListe
{
    public class EditKundeModel : PageModel
    {
        private IKundeRepository _repo;

        public EditKundeModel(IKundeRepository repo)
        {
            _repo = repo;
        }


        [BindProperty]
        public int NytKundeId { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "Der skal være et navn")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal være mindst to tegn i et navn")]
        public string NytKundeNavn { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Der skal være et navn")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal være mindst to tegn i et navn")]
        public string NytKundeEfternavn { get; set; }

        [BindProperty]
        public string NytKundeEmail { get; set; }

        [BindProperty]
        public string NytAdresse { get; set; }

        [BindProperty]
        public int NytPostnr { get; set; }

        [BindProperty]
        public string NytKode { get; set; }

        [BindProperty]
        public bool NytAdmin { get; set; }



        public string ErrorMessage { get; private set; }
        public bool Error { get; private set; }



        public void OnGet(int nummer)
        {
            ErrorMessage = "";
            Error = false;

            try
            {
                Kunde kunde = _repo.GetById(nummer);

                NytKundeId = kunde.KundeId;
                NytKundeNavn = kunde.Navn;
                NytKundeEfternavn = kunde.Efternavn;
            }
            catch (KeyNotFoundException knfe)
            {
                ErrorMessage = knfe.Message;
                Error = true;
            }
        }


        public IActionResult OnPostChange()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Kunde kunde = _repo.Opdater(new Kunde(NytKundeId, NytKundeNavn, NytKundeEfternavn, NytKundeEmail, NytAdresse, NytPostnr, NytKode, NytAdmin));

            return RedirectToPage("Index");
        }



        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
