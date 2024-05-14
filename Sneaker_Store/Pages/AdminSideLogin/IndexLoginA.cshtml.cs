using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages.AdminSideLogin;

public class IndexLoginA : PageModel
{
    private IKundeRepository _kunder;

    public Kunde KundeLoggedIn { get; private set; }

    public IndexLoginA(IKundeRepository kunder)
    {
        _kunder = kunder;
    }

    public IActionResult OnGet()
    {
        if (_kunder is null || _kunder.KundeLoggedIn is null || !_kunder.KundeLoggedIn.Admin)
        {
            return RedirectToPage("/Login");
        }

        KundeLoggedIn = _kunder.KundeLoggedIn;

        return Page();
    }
}