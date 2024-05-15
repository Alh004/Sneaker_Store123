using Sneaker_Store.Model;

namespace Sneaker_Store.Services
{
    public interface IKundeRepository
    {
        Kunde? KundeLoggedIn { get; }
        List<Kunde> GetAll();
        void AddKunde(Kunde kunde);
        bool CheckKunde(string email, string password);
        void LogoutKunde();
        void RemoveKunde(Kunde kunde);
    }
}
