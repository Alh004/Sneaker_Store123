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

        public KurvIndexModel(Kurv kurv, IOrderRepository orderRepository)
        {
            _kurv = kurv;
            _orderRepository = orderRepository;
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

        public IActionResult OnPostKoeb() // Ændret metodenavnet til OnPostKoeb
        {
            // Opret en ny ordre i databasen og få ordre-ID
            int orderId = _orderRepository.CreateOrder();

            // Tilføj de købte sko til denne ordre
            foreach (var sko in _kurv.HentAlleSko())
            {
                _orderRepository.AddSkoToOrder(orderId, sko.SkoId);
            }

            return RedirectToPage("OrderConfirm", new { orderId });
        }
    }
}