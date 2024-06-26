using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages.common.KundeListe
{
    public class DeleteKundeModel : PageModel
    {
        
            private IKundeRepository _repo;

            public DeleteKundeModel(IKundeRepository repo)
            {
                _repo = repo;
            }



            public Kunde Kunde { get; private set; }



            public IActionResult OnGet(int nummer)
            {
                Kunde = _repo.GetById(nummer);

                return Page();
            }

            public IActionResult OnPostDelete(int nummer)
            {
                _repo.Remove(nummer);

                return RedirectToPage("Index");
            }

            public IActionResult OnPostCancel()
            {
                return RedirectToPage("Index");
            }


        }
    }
