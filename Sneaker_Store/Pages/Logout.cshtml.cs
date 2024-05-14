using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages
{
    public class LogoutModel : PageModel
    {
        private IKundeRepository _kundeRepository;

        public LogoutModel(IKundeRepository kundeRepository)
        {
            _kundeRepository = kundeRepository;
        }

        public IActionResult OnGet()
        {
            _kundeRepository.LogoutKunde();

            return RedirectToPage("Index");
        }
    }
}
