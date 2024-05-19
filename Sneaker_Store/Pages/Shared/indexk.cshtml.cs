using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Model;
using Sneaker_Store.Services;

namespace TestSessions.Pages.Personer
{
    public class IndexModel : PageModel
    {
        public Kurv Kurv{ get; set; }

        public PageResult OnGet()
        {
            try
            {
                Kurv = Testsession.Get<Kurv>(HttpContext);
                return Page();
            }
            catch (NoSessionObjectException ne)
            {
                Kurv = new Kurv();
            }


            return null;
        }
    }
}