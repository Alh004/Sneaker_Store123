using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages.KundeListe
{
    public class IndexModel : PageModel
    {
        // instans af kunde repository
        private IKundeRepository _repo;

        // Dependency Injection
        public IndexModel(IKundeRepository repository)
        {
            _repo = repository;
        }


        // property til View'et
        public List<Kunde> Kunder { get; set; }

        [BindProperty]
        public int SearchNumber { get; set; }
        [BindProperty]
        public string SearchName { get; set; }
        [BindProperty]
        public string SearchPhone { get; set; }


        public void OnGet()
        {

            //KundeRepository repo = new KundeRepository(true);

            Kunder = _repo.GetAll();

        }

        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }

        public IActionResult OnPostSearch()
        {
            Kunder = _repo.Search(SearchNumber, SearchName, SearchPhone);
            return Page();
        }

        public IActionResult OnPostSortNumber()
        {
            Kunder = _repo.SortNumber();
            return Page();
        }

        public IActionResult OnPostSortName()
        {
            Kunder = _repo.SortName();
            return Page();
        }
    }
}
