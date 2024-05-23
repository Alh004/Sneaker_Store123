using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
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

}