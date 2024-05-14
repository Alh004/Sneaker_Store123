using Sneaker_Store.Model;

namespace Sneaker_Store.Services;

public class KundeRepository
{
    private List<Kunde> _users = new List<Kunde>();

    public Kunde? UserLoggedIn { get; private set; }

    public KundeRepository(bool mockData = false)
    {
        UserLoggedIn = null;

        if (mockData)
        {
            _users.Add(new Kunde(1,"ali","h","ali@1.dk","øvej","kbh",2450,"test", true));
        }

    }

    public void AddUser(Kunde kunde)
    {
        _users.Add(kunde);
    }

    public void RemoveUser(Kunde kunde)
    {
        _users.Remove(kunde);
    }

    public bool CheckUser(string username, string password)
    {
        Kunde? foundUser = _users.Find(u => u.Navn == username && u.Kode == password);

        if (foundUser != null)
        {
            UserLoggedIn = foundUser;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LogoutUser()
    {
        UserLoggedIn = null;
    }
}