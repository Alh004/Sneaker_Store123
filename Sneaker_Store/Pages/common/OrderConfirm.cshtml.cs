using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sneaker_Store.Services;
using Sneaker_Store.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sneaker_Store.Pages.common
{
    public class OrderConfirmModel : PageModel
    {
        private readonly IKundeRepository _kundeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISkoRepository _skoRepository;

        public OrderConfirmModel(IKundeRepository kundeRepository, IOrderRepository orderRepository, ISkoRepository skoRepository)
        {
            _kundeRepository = kundeRepository;
            _orderRepository = orderRepository;
            _skoRepository = skoRepository;
        }

        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Sko> PurchasedShoes { get; set; }

        public void OnGet(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            var customer = _kundeRepository.GetById(order.KundeId);

            OrderId = order.OrdreId;
            CustomerName = $"{customer.Navn} {customer.Efternavn}";
            CustomerEmail = customer.Email;
            TotalItems = order.Antal;
            TotalPrice = order.TotalPris;

            PurchasedShoes = new List<Sko> { _skoRepository.GetById(order.SkoId) }; // Assuming one type of shoe for simplicity
        }
    }
}