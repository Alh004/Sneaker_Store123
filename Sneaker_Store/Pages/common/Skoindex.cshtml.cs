using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sneaker_Store.Pages.common
{
    public class SkoIndexModel : PageModel
    {
        private readonly ISkoRepository _skoRepository;
        private readonly Kurv _kurv;

        public SkoIndexModel(ISkoRepository skoRepository, Kurv kurv)
        {
            _skoRepository = skoRepository;
            _kurv = kurv;
        }

        public List<Sko> Skos { get; set; }
        [BindProperty] public int SelectedSkoId { get; set; }
        [BindProperty] public string SelectedBrand { get; set; }
        [BindProperty] public string PriceFilter { get; set; }
        public List<string> Brands { get; set; } = new List<string> { "Nike", "ASICS", "Adidas", "Air Jordan", "Puma", "Crocs", "Converse"};

        public void OnGet()
        {
            Skos = _skoRepository.GetAll();
        }

        public void OnPostFilter()
        {
            var skos = _skoRepository.GetAll();

            if (!string.IsNullOrEmpty(SelectedBrand))
            {
                skos = skos.Where(s => s.Maerke == SelectedBrand).ToList();
            }

            if (PriceFilter == "Lowest")
            {
                skos = skos.OrderBy(s => s.Pris).ToList();
            }
            else if (PriceFilter == "Highest")
            {
                skos = skos.OrderByDescending(s => s.Pris).ToList();
            }

            Skos = skos;
        }

        public IActionResult OnPostAddToBasket()
        {
            var selectedSko = _skoRepository.GetById(SelectedSkoId);
            if (selectedSko != null)
            {
                _kurv.Tilføj(selectedSko);
                return RedirectToPage("/common/KurvIndex");
            }

            ModelState.AddModelError(string.Empty, "Selected shoe not found.");
            Skos = _skoRepository.GetAll();
            return Page();
        }
    }
}
