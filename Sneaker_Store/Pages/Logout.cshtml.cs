using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IKundeRepository _kundeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IKundeRepository kundeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _kundeRepository = kundeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            // Log ud af brugeren ved at fjerne deres email fra sessionen
            _httpContextAccessor.HttpContext.Session.Remove("UserEmail");

            return RedirectToPage("/Index");
        }
    }
}