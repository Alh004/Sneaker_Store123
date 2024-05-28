using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using static Sneaker_Store.Model.Kunde;

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


        public void OnGet()
        {
            Kunder = _repo.GetAll();
        }

        public void OnPost()
        {
            Kunder = _repo.GetAll();

            Kunder.Sort(new KundeSortByIdReverse());
            
        }

        public void OnPostSortName()
        {
            Kunder = _repo.GetAll();
            try
            {
                Kunder.Sort();
            }
            catch (Exception ex) { }
        }

        public void OnPostSortNameDB()
        {
            Kunder = _repo.GetAllKunderSortedByNavnReversed();
        }
    }
}

