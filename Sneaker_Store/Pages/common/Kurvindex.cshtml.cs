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
        private readonly Kurv _kurv;
        private readonly IOrderRepository _orderRepository;
        private readonly IKundeRepository _kundeRepository;

        public KurvIndexModel(Kurv kurv, IOrderRepository orderRepository, IKundeRepository kundeRepository)
        {
            _kurv = kurv;
            _orderRepository = orderRepository;
            _kundeRepository = kundeRepository;
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

        public IActionResult OnPostKoeb()
        {
            var kundeEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(kundeEmail))
            {
                return RedirectToPage("/Login");
            }

            var kunde = _kundeRepository.GetByEmail(kundeEmail);
            if (kunde == null)
            {
                return RedirectToPage("/Login");
            }

            foreach (var sko in _kurv.HentAlleSko())
            {
                var ordre = new Ordre
                {
                    KundeId = kunde.KundeId,
                    SkoId = sko.SkoId
                };
                _orderRepository.AddOrdre(ordre);
            }

            _kurv.Køb();

            return RedirectToPage("OrderConfirm", new { orderId = _orderRepository.GetOrdersByCustomerId(kunde.KundeId).LastOrDefault()?.OrdreId });
        }
    }
}