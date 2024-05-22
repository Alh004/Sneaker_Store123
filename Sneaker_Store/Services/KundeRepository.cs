using Sneaker_Store.Model;

namespace Sneaker_Store.Services;

public class KundeRepository : IKundeRepository
{
    private List<Kunde> _kunder = new List<Kunde>();

    public Kunde? KundeLoggedIn { get; private set; }

    public KundeRepository(bool mockData = false)
    {
        KundeLoggedIn = null;

        if (mockData)
        {
            _kunder.Add(new Kunde(1, "ali", "h", "ali@1.dk", "vej", "kbh", 2450, "test", true));
            _kunder.Add(new Kunde(2, "dani", "h", "dani@2.dk", "vej", "kbh", 2450, "test2", false));
        }

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

    

    public void LogoutKunde()
    {
        KundeLoggedIn = null;
    }
    
    
}