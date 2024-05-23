using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IKundeRepository _kundeRepository;

        public LoginModel(IKundeRepository kundeRepository)
        {
            _kundeRepository = kundeRepository;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Kode { get; set; }

        public string ErrorMessage { get; private set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Kode))
            {
                ErrorMessage = "baade email og kode er forkert.";
                return Page();
            }

            if (!_kundeRepository.CheckKunde(Email, Kode))
            {
                ErrorMessage = "Forket kode eller email.";
                return Page();
            }

            return RedirectToPage("/common/Skoindex");
        }
    }
}