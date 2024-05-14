using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private IKundeRepository _repo;

    public IndexModel(ILogger<IndexModel> logger, IKundeRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    public IActionResult OnGet()
    {
        if (_repo is null || _repo.KundeLoggedIn is null)
        {
            return RedirectToPage("/Login");
        }

        return Page();
    }
}