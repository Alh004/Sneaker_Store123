using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages.common
{
    public class KundeIndexModel : PageModel
    {
        private IKundeRepository _kunder;

        public Kunde KundeLoggedIn { get; private set; }

        public KundeIndexModel(IKundeRepository kunder)
        {
            _kunder = kunder;
        }
        
        public IActionResult OnGet()
        {
            if (_kunder is null || _kunder.KundeLoggedIn is null)
            {
                // Redirect to the SkoIndex page
                return RedirectToPage("/common/SkoIndex");
            }

            // You might have other logic here
            return Page();
        }

    }
}
