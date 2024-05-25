using Sneaker_Store.Model;

namespace Sneaker_Store.Services;

public class KundeRepository : IKundeRepository
{
    private List<Kunde> _kunder = new List<Kunde>();

    // konstruktør

    public Kunde? KundeLoggedIn { get; private set; }

    public void PopulateKundeRepository(bool mockData = false)
    {
        KundeLoggedIn = null;

        if (mockData)
        {
            _kunder.Add(new Kunde(1, "ali", "h", "ali@1.dk", "vej", 2450, "test", true));
            _kunder.Add(new Kunde(2, "dani", "h", "dani@2.dk", "vej",  2450, "test2", false));
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

    public void Remove(Kunde kunde)
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

    public Kunde Update(int kundeid, Kunde updatedKunde)
    {
        if (kundeid != updatedKunde.KundeId)
        {
            throw new ArgumentException("kan ikke opdatere kundeid og obj.KundeId er forskellige");
        }

        Kunde updateThisKunde = GetById(kundeid);

        updateThisKunde.Navn = updatedKunde.Navn;
        updateThisKunde.Efternavn = updatedKunde.Efternavn;
        updateThisKunde.Email = updatedKunde.Email;
        updateThisKunde.Adresse = updatedKunde.Adresse;
        updateThisKunde.Postnr = updatedKunde.Postnr;
        updateThisKunde.Kode = updatedKunde.Kode;

        return updateThisKunde;
    }

    public Kunde GetByEmail(string email)
    {
        return _kunder.FirstOrDefault(k => k.Email == email);
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

    public Kunde Update(Kunde kunde)
    {
        throw new NotImplementedException();
    }

    public Kunde Remove(int kundeid)
    {
        Kunde deleteKunde = GetById(kundeid);

        _kunder.Remove(deleteKunde);
        return deleteKunde;
    }
}