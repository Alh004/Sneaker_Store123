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
        public Kunde Opdater(Kunde kunde);
        Kunde GetById(int Kundeid);
        Kunde GetKunde(int kundenummer);
        public Dictionary<int, Kunde> Katalog { get; set; }


        List<Kunde> Search(int? number, string? name, string? phone);
        List<Kunde> SortNumber();
        List<Kunde> SortName();
    }
}
