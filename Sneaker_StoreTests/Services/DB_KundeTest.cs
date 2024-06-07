using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using System.Collections.Generic;

namespace Sneaker_StoreTests.Services
{
    [TestClass]
    public class DB_KundeTest
    {
        private DB_Kunde _dbKunde;

        [TestInitialize]
        public void SetUp()
        {
            _dbKunde = new DB_Kunde();
        }

        [TestMethod]
        public void CheckKunde_ValidCredentials_ReturnsLoginResult()
        {
            // Test med gyldige login-oplysninger
            var email = "sofie.hansen@example.com";
            var kode = "kode1234";

            var result = _dbKunde.CheckKunde(email, kode);

            Assert.IsNotNull(result); // Resultatet må ikke være null
            Assert.IsInstanceOfType(result, typeof(LoginResult)); // Resultatet skal være af typen LoginResult
            Assert.IsFalse(result.IsAdmin); // Sofie Hansen er ikke admin i vores data
        }

        [TestMethod]
        public void CheckKunde_InvalidCredentials_ReturnsNull()
        {
            // Test med ugyldige login-oplysninger
            var email = "invalid@example.com";
            var kode = "forkertkode";

            var result = _dbKunde.CheckKunde(email, kode);

            Assert.IsNull(result); // Resultatet skal være null
        }

        [TestMethod]
        public void GetById_ExistingKundeId_ReturnsKunde()
        {
            // Test for at hente en kunde med et eksisterende ID
            var kundeId = 1; // Gyldigt kundeId

            var kunde = _dbKunde.GetById(kundeId);

            Assert.IsNotNull(kunde); // Kunden må ikke være null
            Assert.AreEqual(kundeId, kunde.KundeId); // KundeId skal matche
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetById_NonExistingKundeId_ThrowsKeyNotFoundException()
        {
            // Test for at hente en kunde med et ikke-eksisterende ID
            var kundeId = -1; // Ugyldigt kundeId

            _dbKunde.GetById(kundeId); // Forventet at kaste en undtagelse
        }

        [TestMethod]
        public void GetAll_ReturnsAllKunder()
        {
            // Test for at hente alle kunder
            var kunder = _dbKunde.GetAll();

            Assert.IsNotNull(kunder); // Resultatet må ikke være null
            Assert.IsTrue(kunder.Count > 0); // Der skal være mindst én kunde
        }

        [TestMethod]
        public void GetByEmail_ExistingEmail_ReturnsKunde()
        {
            // Test for at hente en kunde med en eksisterende email
            var email = "sofie.hansen@example.com";

            var kunde = _dbKunde.GetByEmail(email);

            Assert.IsNotNull(kunde); // Kunden må ikke være null
            Assert.AreEqual(email, kunde.Email); // Email skal matche
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetByEmail_NonExistingEmail_ThrowsKeyNotFoundException()
        {
            // Test for at hente en kunde med en ikke-eksisterende email
            var email = "non_existing_email@example.com";

            _dbKunde.GetByEmail(email); // Forventet at kaste en undtagelse
        }

        [TestMethod]
        public void AddKunde_ValidKunde_AddsKunde()
        {
            // Test for at tilføje en ny kunde
            var kunde = new Kunde
            {
                Navn = "Test",
                Efternavn = "User",
                Email = "newuser@example.com",
                Kode = "password",
                Postnr = 1234,
                Adresse = "Test Street 123",
                Admin = false
            };

            _dbKunde.AddKunde(kunde);

            var addedKunde = _dbKunde.GetByEmail(kunde.Email);
            Assert.IsNotNull(addedKunde); // Kunden må ikke være null
            Assert.AreEqual(kunde.Email, addedKunde.Email); // Email skal matche

            // Oprydning
            _dbKunde.RemoveKunde(addedKunde.KundeId);
        }

        [TestMethod]
        public void RemoveKunde_ExistingKundeId_RemovesKunde()
        {
            // Test for at fjerne en eksisterende kunde
            var kunde = new Kunde
            {
                Navn = "Test",
                Efternavn = "User",
                Email = "toberemoved@example.com",
                Kode = "password",
                Postnr = 1234,
                Adresse = "Test Street 123",
                Admin = false
            };
            _dbKunde.AddKunde(kunde);
            var addedKunde = _dbKunde.GetByEmail(kunde.Email);

            _dbKunde.RemoveKunde(addedKunde.KundeId);

            Assert.ThrowsException<KeyNotFoundException>(() => _dbKunde.GetById(addedKunde.KundeId)); // Forventet at kaste en undtagelse
        }

        [TestMethod]
        public void UpdateKunde_ValidKunde_UpdatesKunde()
        {
            // Test for at opdatere en eksisterende kunde
            var kunde = new Kunde
            {
                Navn = "Test",
                Efternavn = "User",
                Email = "toupdate@example.com",
                Kode = "password",
                Postnr = 1234,
                Adresse = "Test Street 123",
                Admin = false
            };
            _dbKunde.AddKunde(kunde);
            var addedKunde = _dbKunde.GetByEmail(kunde.Email);
            addedKunde.Navn = "Updated Name";

            var updatedKunde = _dbKunde.UpdateKunde(addedKunde.KundeId, addedKunde);

            Assert.AreEqual(addedKunde.Navn, updatedKunde.Navn); // Navnet skal være opdateret

            // Oprydning
            _dbKunde.RemoveKunde(addedKunde.KundeId);
        }

        [TestMethod]
        public void LogoutKunde_SetsKundeLoggedInToNull()
        {
            // Test for at logge en kunde ud
            _dbKunde.LogoutKunde();

            Assert.IsNull(_dbKunde.KundeLoggedIn); // KundeLoggedIn skal være null
        }

        [TestMethod]
        public void GetAllKunderSortedByNavnReversed_ReturnsKunderSortedByNavnDescending()
        {
            // Test for at hente alle kunder sorteret efter navn i faldende rækkefølge
            var kunder = _dbKunde.GetAllKunderSortedByNavnReversed();

            Assert.IsNotNull(kunder); // Resultatet må ikke være null
            Assert.IsTrue(kunder.SequenceEqual(kunder.OrderByDescending(k => k.Navn))); // Kunderne skal være sorteret korrekt
        }
    }
}
