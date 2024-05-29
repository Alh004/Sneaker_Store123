using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IKundeRepository _kundeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IKundeRepository kundeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _kundeRepository = kundeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty] 
        public string Email { get; set; }

        [BindProperty] 
        public string Kode { get; set; }

        public string ErrorMessage { get; private set; }

        public IActionResult OnGet()
        {
            // Kontroller om brugeren allerede er logget ind
            if (_httpContextAccessor.HttpContext.Session.GetString("UserEmail") != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Kode))
            {
                ErrorMessage = "Både email og kode skal udfyldes.";
                return Page();
            }

            var loginResult = _kundeRepository.CheckKunde(Email, Kode);

            if (loginResult == null)
            {
                ErrorMessage = "Forkert kode eller email.";
                return Page();
            }

            // Gem brugerens email i sessionen
            _httpContextAccessor.HttpContext.Session.SetString("UserEmail", Email);
            
            if (loginResult.IsAdmin)
            {
                return RedirectToPage("/AdminSideLogin/IndexLoginA"); // Redirect til admin siden
            }
            else
            {
                return RedirectToPage("/common/KundeIndex"); // Redirect til regular bruger siden
            }
        }
    }
}