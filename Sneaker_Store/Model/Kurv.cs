using Microsoft.AspNetCore.Http;
using Sneaker_Store.Services;
using System.Collections.Generic;

namespace Sneaker_Store.Model
{
    public class Kurv
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Kurv(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
    }
}