using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;

namespace Sneaker_Store.Pages;

public class IndexModel : PageModel
{
    private readonly IKundeRepository _kundeRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IndexModel(IKundeRepository kundeRepository, IHttpContextAccessor httpContextAccessor)
    {
        _kundeRepository = kundeRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public Kunde LoggedInKunde { get; private set; }

    public void OnGet()
    {
        string email = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
        if (!string.IsNullOrEmpty(email))
        {
            LoggedInKunde = _kundeRepository.GetByEmail(email);
        }
    }
}