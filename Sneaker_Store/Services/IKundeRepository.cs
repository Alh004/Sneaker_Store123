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
        void RemoveKunde(Kunde kunde);
        Kunde GetById(int Kundeid);
        Kunde Opdater(Kunde kunde);
        List<Kunde> Search(int number, string name, string phone);
        List<Kunde> SortNumber();
        List<Kunde> SortName();
        Kunde Remove(int kundeid);
        Kunde Update(int nytKundeId, Kunde kunde);
        Kunde GetByEmail(string email);
    }
}