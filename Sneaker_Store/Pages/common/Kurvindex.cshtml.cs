using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sneaker_Store.Pages.common
{
    public class KurvIndexModel : PageModel
    {
        private readonly ISkoRepository _skoRepository;
        private readonly Kurv _kurv;

        public KurvIndexModel(ISkoRepository skoRepository, Kurv kurv)
        {
            _skoRepository = skoRepository;
            _kurv = kurv;
        }

        public List<Sko> Skos { get; set; }

        public IActionResult OnGet()
        {
            Skos = _kurv.HentAlleSko();
            return Page();
        }

        public IActionResult OnPostDeleteFromBasket(int SkoId)
        {
            var sko = _kurv.HentAlleSko().FirstOrDefault(s => s.SkoId == SkoId);
            if (sko != null)
            {
                _kurv.Slet(sko);
            }

            Skos = _kurv.HentAlleSko();
            return RedirectToPage();
        }
    }
}