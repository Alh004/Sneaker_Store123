using Sneaker_Store.Model;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public interface IKundeRepository
    {
        Kunde? KundeLoggedIn { get; }
        List<Kunde> GetAll();
        void AddKunde(Kunde kunde);
        LoginResult? CheckKunde(string email, string kode);
        void LogoutKunde();
        public Kunde Remove(int kundeid);
        public Kunde GetById(int Kundeid);
        public Kunde Update(int nytKundeId, Kunde kunde);
        public Kunde GetByEmail(string email);

        List<Kunde> GetAllKunderSortedByNavnReversed();
    }
}