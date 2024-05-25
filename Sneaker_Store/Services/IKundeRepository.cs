using Sneaker_Store.Model;

namespace Sneaker_Store.Services
{
    public interface IKundeRepository
    {
        Kunde? KundeLoggedIn { get; }
        List<Kunde> GetAll();
        void AddKunde(Kunde kunde);
        bool CheckKunde(string email, string kode);
        void LogoutKunde();
        void RemoveKunde(Kunde kunde);
        public Kunde GetById(int Kundeid);
        public Kunde Opdater(Kunde kunde);

          Kunde GetByEmail(string email);


        public List<Kunde> Search(int number, string name, string phone);
        public List<Kunde> SortNumber();
        public List<Kunde> SortName();
    }
}
