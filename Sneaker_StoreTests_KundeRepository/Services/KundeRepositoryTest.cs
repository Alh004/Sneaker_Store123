using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using System.Collections.Generic;
using System;

namespace Sneaker_Store.Tests
{
    [TestClass]
    public class KundeRepositoryTests
    {
        private KundeRepository kundeRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            kundeRepository = new KundeRepository();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Ryd op i testdata
            CleanupKunde("test.user@example.com");
            CleanupKunde("remove.user@example.com");
            CleanupKunde("original.name@example.com");
            CleanupKunde("updated.email@example.com");
        }

        private void CleanupKunde(string email)
        {
            try
            {
                var kunde = kundeRepository.GetByEmail(email);
                if (kunde != null)
                {
                    kundeRepository.RemoveKunde(kunde.KundeId);
                }
            }
            catch (KeyNotFoundException)
            {
                // Kunde ikke fundet, intet at rydde op
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CleanupKunde Exception: {ex.Message}");
            }
        }

        [TestMethod]
        public void AddKunde_SkalTilføjeNyKunde()
        {
            // Arrange
            var nyKunde = new Kunde
            {
                Navn = "Test",
                Efternavn = "User",
                Email = "test.user@example.com",
                Adresse = "Test Address",
                Postnr = 12345,
                Kode = "testpassword",
                Admin = false
            };

            // Act
            kundeRepository.AddKunde(nyKunde);
            var tilføjetKunde = kundeRepository.GetByEmail(nyKunde.Email);

            // Assert
            Assert.IsNotNull(tilføjetKunde);
            Assert.AreEqual(nyKunde.Email, tilføjetKunde.Email);
            Assert.AreEqual(nyKunde.Navn, tilføjetKunde.Navn);
            Assert.AreEqual(nyKunde.Efternavn, tilføjetKunde.Efternavn);
            Assert.AreEqual(nyKunde.Adresse, tilføjetKunde.Adresse);
            Assert.AreEqual(nyKunde.Postnr, tilføjetKunde.Postnr);
            Assert.AreEqual(nyKunde.Kode, tilføjetKunde.Kode);
            Assert.AreEqual(nyKunde.Admin, tilføjetKunde.Admin);
        }

        [TestMethod]
        public void UpdateKunde_SkalOpdatereKundeOplysninger()
        {
            // Arrange
            var nyKunde = new Kunde
            {
                Navn = "Original",
                Efternavn = "Name",
                Email = "original.name@example.com",
                Adresse = "Original Address",
                Postnr = 12345,
                Kode = "originalpassword",
                Admin = false
            };

            kundeRepository.AddKunde(nyKunde);
            var tilføjetKunde = kundeRepository.GetByEmail(nyKunde.Email);

            Console.WriteLine($"Tilføjet Kunde ID: {tilføjetKunde.KundeId}");

            var opdateretKunde = new Kunde
            {
                KundeId = tilføjetKunde.KundeId,
                Navn = "Updated",
                Efternavn = "Name",
                Email = "updated.email@example.com",
                Adresse = "Updated Address",
                Postnr = 67890,
                Kode = "newpassword",
                Admin = true
            };

            // Act
            kundeRepository.UpdateKunde(tilføjetKunde.KundeId, opdateretKunde);
            var resultat = kundeRepository.GetById(tilføjetKunde.KundeId);

            // Assert
            Assert.IsNotNull(resultat);
            Assert.AreEqual(opdateretKunde.Navn, resultat.Navn);
            Assert.AreEqual(opdateretKunde.Efternavn, resultat.Efternavn);
            Assert.AreEqual(opdateretKunde.Email, resultat.Email);
            Assert.AreEqual(opdateretKunde.Adresse, resultat.Adresse);
            Assert.AreEqual(opdateretKunde.Postnr, resultat.Postnr);
            Assert.AreEqual(opdateretKunde.Kode, resultat.Kode);
            Assert.AreEqual(opdateretKunde.Admin, resultat.Admin);
        }

        [TestMethod]
        public void GetById_SkalReturnereKorrektKunde()
        {
            // Arrange
            int kundeId = 1; // Sørg for, at denne ID eksisterer i din testdatabase

            // Act
            var kunde = kundeRepository.GetById(kundeId);

            // Assert
            Assert.IsNotNull(kunde);
            Assert.AreEqual(kundeId, kunde.KundeId);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetById_SkalKasteExceptionNårKundeIkkeFindes()
        {
            // Arrange
            int kundeId = -1;

            // Act
            var kunde = kundeRepository.GetById(kundeId);

            // Assert håndteres af ExpectedException attributten
        }

        [TestMethod]
        public void GetAll_SkalReturnereAlleKunder()
        {
            // Arrange & Act
            var kunder = kundeRepository.GetAll();

            // Assert
            Assert.IsNotNull(kunder);
            Assert.IsTrue(kunder.Count > 0); // Antagelse om at der er mindst en kunde i databasen
        }

        [TestMethod]
        public void GetByEmail_SkalReturnereKundeVedKorrektEmail()
        {
            // Arrange
            var nyKunde = new Kunde
            {
                Navn = "Test",
                Efternavn = "User",
                Email = "test.user@example.com",
                Adresse = "Test Address",
                Postnr = 12345,
                Kode = "testpassword",
                Admin = false
            };

            kundeRepository.AddKunde(nyKunde);

            // Act
            var kunde = kundeRepository.GetByEmail(nyKunde.Email);

            // Assert
            Assert.IsNotNull(kunde, "Kunde blev ikke fundet i databasen.");
            Assert.AreEqual(nyKunde.Email, kunde.Email, "Email matcher ikke.");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetByEmail_SkalKasteExceptionNårEmailIkkeFindes()
        {
            // Arrange
            string email = "nonexistent@example.com";

            // Act
            var kunde = kundeRepository.GetByEmail(email);

            // Assert håndteres af ExpectedException attributten
        }

        [TestMethod]
        public void CheckKunde_SkalReturnereLoginResultNårLoginErSuccesfuld()
        {
            // Arrange
            var nyKunde = new Kunde
            {
                Navn = "Test",
                Efternavn = "User",
                Email = "test.user@example.com",
                Adresse = "Test Address",
                Postnr = 12345,
                Kode = "testpassword",
                Admin = false
            };

            kundeRepository.AddKunde(nyKunde);

            // Act
            var loginResult = kundeRepository.CheckKunde(nyKunde.Email, nyKunde.Kode);

            // Assert
            Assert.IsNotNull(loginResult);
            Assert.IsNotNull(kundeRepository.KundeLoggedIn);
            Assert.AreEqual(nyKunde.Email, kundeRepository.KundeLoggedIn.Email);
        }

        [TestMethod]
        public void CheckKunde_SkalReturnereNullNårLoginFejler()
        {
            // Arrange
            string email = "invalid.email@example.com";
            string kode = "wrongpassword";

            // Act
            var loginResult = kundeRepository.CheckKunde(email, kode);

            // Assert
            Assert.IsNull(loginResult);
            Assert.IsNull(kundeRepository.KundeLoggedIn);
        }

        [TestMethod]
        public void RemoveKunde_SkalFjerneKunde()
        {
            // Arrange
            var nyKunde = new Kunde
            {
                Navn = "Test",
                Efternavn = "User",
                Email = "remove.user@example.com",
                Adresse = "Remove Address",
                Postnr = 12345,
                Kode = "removepassword",
                Admin = false
            };

            kundeRepository.AddKunde(nyKunde);
            var tilføjetKunde = kundeRepository.GetByEmail(nyKunde.Email);

            // Act
            kundeRepository.RemoveKunde(tilføjetKunde.KundeId);

            // Assert
            Assert.ThrowsException<KeyNotFoundException>(() => kundeRepository.GetById(tilføjetKunde.KundeId));
        }

        [TestMethod]
        public void LogoutKunde_SkalLoggeUdKunde()
        {
            // Arrange
            kundeRepository.KundeLoggedIn = new Kunde
            {
                Navn = "Test",
                Efternavn = "User",
                Email = "test.user@example.com",
                Kode = "testpassword",
                Admin = false
            };

            // Act
            kundeRepository.LogoutKunde();

            // Assert
            Assert.IsNull(kundeRepository.KundeLoggedIn);
        }

        [TestMethod]
        public void GetAllKunderSortedByNavnReversed_SkalReturnereSorteredeKunder()
        {
            // Arrange
            var kunder = kundeRepository.GetAll();
            var forventetSorteretListe = new List<Kunde>(kunder);
            forventetSorteretListe.Sort((x, y) => string.Compare(y.Navn, x.Navn, StringComparison.Ordinal));

            // Act
            var sorteredeKunder = kundeRepository.GetAllKunderSortedByNavnReversed();

            // Assert
            Assert.AreEqual(forventetSorteretListe.Count, sorteredeKunder.Count);
            for (int i = 0; i < forventetSorteretListe.Count; i++)
            {
                Assert.AreEqual(forventetSorteretListe[i].KundeId, sorteredeKunder[i].KundeId);
                Assert.AreEqual(forventetSorteretListe[i].Navn, sorteredeKunder[i].Navn);
                Assert.AreEqual(forventetSorteretListe[i].Efternavn, sorteredeKunder[i].Efternavn);
                Assert.AreEqual(forventetSorteretListe[i].Email, sorteredeKunder[i].Email);
                Assert.AreEqual(forventetSorteretListe[i].Adresse, sorteredeKunder[i].Adresse);
                Assert.AreEqual(forventetSorteretListe[i].Postnr, sorteredeKunder[i].Postnr);
                Assert.AreEqual(forventetSorteretListe[i].Kode, sorteredeKunder[i].Kode);
                Assert.AreEqual(forventetSorteretListe[i].Admin, sorteredeKunder[i].Admin);
            }
        }
    }
}
