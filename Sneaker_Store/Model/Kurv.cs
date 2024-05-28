using Microsoft.AspNetCore.Http;
using Sneaker_Store.Services;
using System.Collections.Generic;

namespace Sneaker_Store.Model
{
    public class Kurv
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;
        private readonly IKundeRepository _kundeRepository;

        public Kurv(IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, IKundeRepository kundeRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _kundeRepository = kundeRepository;
        }

        public void Tilføj(Sko sko)
        {
            var items = HentAlleSko();
            items.Add(sko);
            SaveToSession(items);
        }

        public List<Sko> HentAlleSko()
        {
            try
            {
                return Testsession.Get<List<Sko>>(_httpContextAccessor.HttpContext);
            }
            catch (NoSessionObjectException)
            {
                return new List<Sko>();
            }
        }

        public void Slet(Sko sko)
        {
            var items = HentAlleSko();
            items.RemoveAll(s => s.SkoId == sko.SkoId);
            SaveToSession(items);
        }

        private void SaveToSession(List<Sko> items)
        {
            Testsession.Set(items, _httpContextAccessor.HttpContext);
        }

        public int Køb()
        {
            var items = HentAlleSko();
            var email = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidOperationException("Ingen kunde er logget ind.");
            }

            var customer = _kundeRepository.GetByEmail(email);
            if (customer == null)
            {
                throw new InvalidOperationException("Kunde ikke fundet.");
            }

            foreach (var sko in items)
            {
                var order = new Ordre
                {
                    KundeId = customer.KundeId,
                    SkoId = sko.SkoId,
                    Antal = 1, // Assuming one item per order for simplicity
                    TotalPris = sko.Pris
                };
                _orderRepository.AddOrdre(order);
            }

            var tomKurv = new List<Sko>();
            SaveToSession(tomKurv);
            
            // Return the last order ID for confirmation page
            return items.Last().SkoId;
        }
    }
}
