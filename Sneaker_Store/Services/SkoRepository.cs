using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;
using static Sneaker_Store.Services.SkoRepository;

namespace Sneaker_Store.Services
{
    public class SkoRepository:ISkoRepository
    {
            private List<Sko> _sko;

            public SkoRepository(bool mockData = false)
            {
                _sko = new List<Sko>();

                if (mockData)
                {
                    PopulateSko();
                }
            }

            private void PopulateSko()
            {
                _sko.Add(new Sko(1, "Nike","Air Max",44, 1000));
                _sko.Add(new Sko(2, "Asics", "Gel-1130", 38, 850));
                _sko.Add(new Sko(3, "Adidas", "Campus", 42, 700));
                _sko.Add(new Sko(4, "Asics", "Gel-Kayano", 44, 600));
                _sko.Add(new Sko(5, "SneakerCare", "SneakerCare", 0, 49));
            }

            public List<Sko> GetAll()
            {
                return new List<Sko>(_sko);
            }

            public Sko GetById(int skoid)
            {
                Sko? sko = _sko.Find(s => s.SkoId == skoid);
                if (sko is null)
                {
                    throw new KeyNotFoundException();
                }
                return sko;
            }
            public Sko Add(Sko sko)
            {
                _sko.Add(sko);
                return sko;
            }

            public Sko Delete(int skoid)
            {
                Sko deleteSko = GetById(skoid);

                _sko.Remove(deleteSko);
                return deleteSko;
            }

            public Sko Update(int skoid, Sko updatedSko)
            {
                if (skoid != updatedSko.SkoId)
                {
                    throw new ArgumentException("kan ikke opdatere id og obj.Id er forskellige");
                }

                Sko updateThisSko = GetById(skoid);

                updateThisSko.Maerke = updatedSko.Maerke;
                updateThisSko.Model = updatedSko.Model;
                updateThisSko.Str = updatedSko.Str;
                updateThisSko.Pris = updatedSko.Pris;

                return updateThisSko;
            }

            public Sko ReadSko(SqlDataReader reader)
            {
                throw new NotImplementedException();
            }

        
           
    }
}
