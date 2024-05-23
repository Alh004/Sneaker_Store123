using Sneaker_Store.Model;

namespace Sneaker_Store.Services;

public class KundeRepository : IKundeRepository
{
    private List<Kunde> _kunder = new List<Kunde>();

    Dictionary<int, Kunde> _katalog;

    // properties
    public Dictionary<int, Kunde> Katalog
    {
        get { return _katalog; }
        set { _katalog = value; }
    }

    // konstruktør
    public KundeRepository(bool mockData = false)
    {
        _katalog = new Dictionary<int, Kunde>();


        if (mockData)
        {
            PopulateKundeRepository();
        }
    }

    public Kunde? KundeLoggedIn { get; private set; }

    public void PopulateKundeRepository(bool mockData = false)
    {
        KundeLoggedIn = null;

        if (mockData)
        {
            _kunder.Add(new Kunde(1, "ali", "h", "ali@1.dk", "vej", "kbh", 2450, "test", true));
            _kunder.Add(new Kunde(2, "dani", "h", "dani@2.dk", "vej", "kbh", 2450, "test2", false));
        }

    }

    public Kunde GetKunde(int kundeid)
    {
        if (_katalog.ContainsKey(kundeid))
        {
            return _katalog[kundeid];
        }
        else
        {
            // opdaget en fejl
            throw new KeyNotFoundException($"kundenummer {kundeid} findes ikke");
        }
    }


    public Kunde GetById(int Kundeid)
    {
        Kunde? kunde = _kunder.Find(k => k.KundeId == Kundeid);
        if (kunde is null)
        {
            throw new KeyNotFoundException();
        }
        return kunde;
    }

    public List<Kunde> GetAll()
    {
        return new List<Kunde>(_kunder);
    }

    public void AddKunde(Kunde kunde)
    {
        _kunder.Add(kunde);
    }

    public void RemoveKunde(Kunde kunde)
    {
        _kunder.Remove(kunde);
    }

    
    
public bool CheckKunde(string email, string kode)
    {
        Kunde? foundKunde = _kunder.Find(u => u.Email == email && u.Kode == kode);

        if (foundKunde != null)
        {
            KundeLoggedIn = foundKunde;
            return true;
        }
        else
        {
            return false;
        }
    }

    public Kunde Opdater(Kunde kunde)
    {
        Kunde editKunde = GetById(kunde.KundeId);
        _kunder[kunde.KundeId] = kunde;
        return kunde;
    }

    public List<Kunde> Search(int? number, string? name, string? phone)
    {
        throw new NotImplementedException();
    }

    public List<Kunde> Search(int number, string name, string phone)
    {
        throw new NotImplementedException();
    }

    public List<Kunde> SortNumber()
    {
        throw new NotImplementedException();
    }

    public List<Kunde> SortName()
    {
        throw new NotImplementedException();
    }


    public void LogoutKunde()
    {
        KundeLoggedIn = null;
    }
    
    
}