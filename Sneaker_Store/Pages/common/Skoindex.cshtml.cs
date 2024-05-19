using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using System.Collections.Generic;

namespace Sneaker_Store.Pages
{
    public class SkoIndexModel : PageModel
    {
        private readonly ISkoRepository _skoRepository;

        public SkoIndexModel(ISkoRepository skoRepository)
        {
            _skoRepository = skoRepository;
        }

        public List<Sko> Skos { get; set; }
        [BindProperty]
        public int SelectedSkoId { get; set; }
        [BindProperty]
        public string SelectedSize { get; set; }
        public List<string> Sizes { get; set; } = new List<string> { "38", "39", "40", "41", "42", "43", "44", "45" };

        public void OnGet()
        {
            Skos = _skoRepository.GetAll();
        }

        public IActionResult OnPost()
        {
            // Handle the purchase logic here
            var selectedSko = _skoRepository.GetById(SelectedSkoId);
            if (selectedSko != null)
            {
                // Process the purchase (e.g., save to database, update stock, etc.)
                // Redirect to a confirmation page or show a success message
                return RedirectToPage("PurchaseConfirmation", new { id = SelectedSkoId, size = SelectedSize });
            }

            // If the selected shoe is not found, reload the page with an error message
            ModelState.AddModelError(string.Empty, "Selected shoe not found.");
            Skos = _skoRepository.GetAll();
            return Page();
            
        }
    }
}