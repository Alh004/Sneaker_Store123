using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using System.Collections.Generic;

public class SkoRepository : ISkoRepository
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
            // Adding multiple sizes for the same shoe model
            _sko.Add(new Sko(1, "Nike", "Air Max", 44, 1000, "https://images.stockx.com/images/Nike-Air-Max-1-97-Sean-Wotherspoon-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1612283442"));
            _sko.Add(new Sko(2, "Nike", "Air Max", 42, 1000, "https://images.stockx.com/images/Nike-Air-Max-1-97-Sean-Wotherspoon-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1612283442"));
            _sko.Add(new Sko(3, "Nike", "Air Max", 41, 1000, "https://images.stockx.com/images/Nike-Air-Max-1-97-Sean-Wotherspoon-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1612283442"));

            _sko.Add(new Sko(4, "Asics", "Gel", 38, 850, "https://images.stockx.com/images/ASICS-GT-2160-COSTS-Shao-Ji-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1702657796"));
            _sko.Add(new Sko(5, "Asics", "Gel", 39, 850, "https://images.stockx.com/images/ASICS-GT-2160-COSTS-Shao-Ji-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1702657796"));

            _sko.Add(new Sko(6, "Adidas", "Campus", 42, 700, "https://images.stockx.com/images/adidas-Campus-00s-Core-Black-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1681283876"));
            _sko.Add(new Sko(7, "Adidas", "Campus", 44, 700, "https://images.stockx.com/images/adidas-Campus-00s-Core-Black-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1681283876"));

            _sko.Add(new Sko(8, "Asics", "Kayano", 44, 600, "https://images.stockx.com/images/ASICS-Gel-1130-Kith-Cream-Scarab-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1688755654"));
            _sko.Add(new Sko(9, "Asics", "Kayano", 40, 600, "https://images.stockx.com/images/ASICS-Gel-1130-Kith-Cream-Scarab-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1688755654"));

            _sko.Add(new Sko(10, "Nike", "Air Force 1", 40, 950, "https://images.stockx.com/images/Nike-Air-Force-1-Low-Supreme-Box-Logo-Black-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1606325289"));
            _sko.Add(new Sko(11, "Nike", "Air Force 1", 42, 950, "https://images.stockx.com/images/Nike-Air-Force-1-Low-Supreme-Box-Logo-Black-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1606325289"));

            _sko.Add(new Sko(12, "Adidas", "Ultra", 43, 1200, "https://images.stockx.com/images/adidas-Ultra-Boost-4-Bape-Camo-Black-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1610084239"));
            _sko.Add(new Sko(13, "Adidas", "Ultra", 44, 1200, "https://images.stockx.com/images/adidas-Ultra-Boost-4-Bape-Camo-Black-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1610084239"));

            _sko.Add(new Sko(14, "Puma", "Suede", 41, 650, "https://images.stockx.com/images/Puma-Suede-One-Piece-Blackbeard-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1712178162"));
            _sko.Add(new Sko(15, "Puma", "Suede", 42, 650, "https://images.stockx.com/images/Puma-Suede-One-Piece-Blackbeard-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1712178162"));

            _sko.Add(new Sko(16, "Reebok", "Classic", 39, 500, "https://images.stockx.com/images/Reebok-Classic-Leather-END-The-Streets-Chalk.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1713297495"));
            _sko.Add(new Sko(17, "Reebok", "Classic", 40, 500, "https://images.stockx.com/images/Reebok-Classic-Leather-END-The-Streets-Chalk.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1713297495"));

            _sko.Add(new Sko(18, "Converse", "Big", 42, 550, "https://images.stockx.com/360/Converse-Chuck-Taylor-All-Star-70s-Ox-Black-White/Images/Converse-Chuck-Taylor-All-Star-70s-Ox-Black-White/Lv2/img01.jpg?fm=webp&auto=compress&w=480&dpr=2&updated_at=1646687536&h=320&q=60"));
            _sko.Add(new Sko(19, "Converse", "Big", 43, 550, "https://images.stockx.com/360/Converse-Chuck-Taylor-All-Star-70s-Ox-Black-White/Images/Converse-Chuck-Taylor-All-Star-70s-Ox-Black-White/Lv2/img01.jpg?fm=webp&auto=compress&w=480&dpr=2&updated_at=1646687536&h=320&q=60"));

            _sko.Add(new Sko(20, "Vans", "Old Skool", 43, 750, "https://images.stockx.com/images/Vans-Old-Skool-Black-White-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1607043282"));
            _sko.Add(new Sko(21, "Vans", "Old Skool", 44, 750, "https://images.stockx.com/images/Vans-Old-Skool-Black-White-Product.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1607043282"));

            _sko.Add(new Sko(22, "SneakerCare", "", null, 49, "https://m.media-amazon.com/images/I/81U-PQD-MBL._AC_UY1000_.jpg")); // Set size to null
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
