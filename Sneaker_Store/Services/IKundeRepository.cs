using Sneaker_Store.Model;

namespace Sneaker_Store.Services
{
    public interface IKundeRepository
    {
        Kunde? KundeLoggedIn { get; }
        List<Kunde> GetAll();
        void AddKunde(Kunde kunde);
        LoginResult? CheckKunde(string email, string kode);
        void LogoutKunde();
        void RemoveKunde(Kunde kunde);
        Kunde GetById(int Kundeid);
        Kunde Opdater(Kunde kunde);
        List<Kunde> Search(int number, string name, string phone);
        List<Kunde> SortNumber();
        List<Kunde> SortName();
        public Kunde Remove(int kundeid);
        public Kunde GetById(int Kundeid);
        public Kunde Update(int nytKundeId, Kunde kunde);

          Kunde GetByEmail(string email);


        public List<Kunde> Search(int number, string name, string phone);
        public List<Kunde> SortNumber();
        public List<Kunde> SortName();
    }
}