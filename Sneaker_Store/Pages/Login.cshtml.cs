using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages
{
    public class LoginModel : PageModel
    {
        private IKundeRepository _kundeRepository;

        public LoginModel(IKundeRepository kundeRepository)
        {
            _kundeRepository = kundeRepository;
        }



        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Kode { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (Email == null || Kode == null)
            {
                return Page();
            }

            if (!_kundeRepository.CheckKunde(Email, Kode))
            {
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
