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

        public void OnGet()
        {
            Skos = _skoRepository.GetAll();
        }
    }
}

