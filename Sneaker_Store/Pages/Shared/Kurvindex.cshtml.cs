using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using System.Collections.Generic;

namespace TestSessions.Pages.Personer
{
    public class IndexModel : PageModel
    {
        private readonly ISkoRepository _skoRepository;
        public readonly Kurv _kurv; // Inject Kurv dependency

        public IndexModel(ISkoRepository skoRepository, Kurv kurv)
        {
            _skoRepository = skoRepository;
            _kurv = kurv; // Store the injected Kurv instance
        }

        public List<Sko> Skos { get; set; } // Add Skos property

        public IActionResult OnGet()
        {
            // Retrieve the list of shoes from the repository
            Skos = _skoRepository.GetAll();

            return Page();
        }

        // Handle POST request to add item to basket
        public IActionResult OnPostAddToBasket(int skoId)
        {
            // Find the shoe with the given ID
            Sko selectedSko = Skos.Find(s => s.SkoId == skoId);
            if (selectedSko != null)
            {
                // Add the shoe to the basket
                _kurv.Tilføj(selectedSko);
            }

            // Redirect back to the index page
            return RedirectToPage();
        }
    }
}