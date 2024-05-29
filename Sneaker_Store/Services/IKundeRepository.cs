using Sneaker_Store.Model;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public interface IKundeRepository
    {
        Kunde? KundeLoggedIn { get; }
        void AddKunde(Kunde kunde);
        Kunde GetById(int kundeId);
        List<Kunde> GetAll();
        Kunde GetByEmail(string email);
        LoginResult? CheckKunde(string email, string kode);
        void RemoveKunde(int kundeId);
        Kunde UpdateKunde(int kundeId, Kunde kunde);
        void LogoutKunde();
        Kunde Remove(int kundeId);
        Kunde Update(int nytKundeId, Kunde kunde);
        List<Kunde> GetAllKunderSortedByNavnReversed();
    }
}