using Microsoft.AspNetCore.Http;
using Sneaker_Store.Services;
using System.Collections.Generic;
using System.Linq;

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
            var kunde = _kundeRepository.KundeLoggedIn;
            if (kunde == null)
            {
                throw new InvalidOperationException("Customer is not logged in.");
            }

            var cartItems = HentAlleSko();
            var totalPris = cartItems.Sum(item => item.Pris);
            var totalAntal = cartItems.Count;

            var ordre = new Ordre
            {
                KundeId = kunde.KundeId,
                SkoId = cartItems.First().SkoId, // Assuming all items are the same shoe for simplicity
                Antal = totalAntal,
                TotalPris = totalPris
            };
            _orderRepository.AddOrdre(ordre);

            var tomKurv = new List<Sko>(); // Create a new empty cart
            SaveToSession(tomKurv); // Save the empty cart in session

            return ordre.OrdreId;
        }
    }
}
