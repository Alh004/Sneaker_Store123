using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sneaker_Store.Model;

namespace Sneaker_Store.Tests
{
    [TestClass]
    public class KundeTests
    {
        [TestMethod]
        public void Kunde_DefaultConstructor_SkalInitialisereEgenskaber()
        {
            // Test for standardkonstruktør initialisering
            var kunde = new Kunde();

            Assert.AreEqual(0, kunde.KundeId);
            Assert.AreEqual(string.Empty, kunde.Navn);
            Assert.AreEqual(string.Empty, kunde.Efternavn);
            Assert.AreEqual(string.Empty, kunde.Email);
            Assert.AreEqual(string.Empty, kunde.Adresse);
            Assert.AreEqual(0, kunde.Postnr);
            Assert.AreEqual(string.Empty, kunde.Kode);
            Assert.IsFalse(kunde.Admin);
        }

        [TestMethod]
        public void Kunde_ParameterizedConstructor_SkalInitialisereEgenskaber()
        {
            // Test for konstruktør med parametre
            int kundeId = 1;
            string navn = "John";
            string efternavn = "Doe";
            string email = "john.doe@example.com";
            string adresse = "123 Main St";
            int postnr = 12345;
            string kode = "password";
            bool admin = true;

            var kunde = new Kunde(kundeId, navn, efternavn, email, adresse, postnr, kode, admin);

            Assert.AreEqual(kundeId, kunde.KundeId);
            Assert.AreEqual(navn, kunde.Navn);
            Assert.AreEqual(efternavn, kunde.Efternavn);
            Assert.AreEqual(email, kunde.Email);
            Assert.AreEqual(adresse, kunde.Adresse);
            Assert.AreEqual(postnr, kunde.Postnr);
            Assert.AreEqual(kode, kunde.Kode);
            Assert.AreEqual(admin, kunde.Admin);
        }

        [TestMethod]
        public void Kunde_SetAndGetProperties_SkalFungereKorrekt()
        {
            // Test for get- og set-metoder
            var kunde = new Kunde();
            int kundeId = 1;
            string navn = "John";
            string efternavn = "Doe";
            string email = "john.doe@example.com";
            string adresse = "123 Main St";
            int postnr = 12345;
            string kode = "password";
            bool admin = true;

            kunde.KundeId = kundeId;
            kunde.Navn = navn;
            kunde.Efternavn = efternavn;
            kunde.Email = email;
            kunde.Adresse = adresse;
            kunde.Postnr = postnr;
            kunde.Kode = kode;
            kunde.Admin = admin;

            Assert.AreEqual(kundeId, kunde.KundeId);
            Assert.AreEqual(navn, kunde.Navn);
            Assert.AreEqual(efternavn, kunde.Efternavn);
            Assert.AreEqual(email, kunde.Email);
            Assert.AreEqual(adresse, kunde.Adresse);
            Assert.AreEqual(postnr, kunde.Postnr);
            Assert.AreEqual(kode, kunde.Kode);
            Assert.AreEqual(admin, kunde.Admin);
        }

        [TestMethod]
        public void Kunde_ToString_SkalReturnereKorrektStreng()
        {
            // Test for ToString metode
            var kunde = new Kunde(1, "John", "Doe", "john.doe@example.com", "123 Main St", 12345, "password", true);
            var forventetStreng = "_kundeId: 1, _navn: John, _efternavn: Doe, _email: john.doe@example.com, _adresse: 123 Main St, _postnr: 12345, _kode: password, _admin: True";

            var resultat = kunde.ToString();

            Assert.AreEqual(forventetStreng, resultat);
        }

        [TestMethod]
        public void Kunde_CompareTo_SkalSortereEfterNavn()
        {
            // Test for CompareTo metode (sortering efter navn)
            var kunde1 = new Kunde(1, "Alice", "Smith", "alice@example.com", "123 Main St", 12345, "password", false);
            var kunde2 = new Kunde(2, "Bob", "Jones", "bob@example.com", "456 Elm St", 67890, "password", false);

            var resultat = kunde1.CompareTo(kunde2);

            Assert.IsTrue(resultat < 0); // Alice kommer før Bob
        }

        [TestMethod]
        public void KundeSortByIdReverse_Compare_SkalSortereEfterIdFaldende()
        {
            // Test for KundeSortByIdReverse (sortering efter ID faldende)
            var kunde1 = new Kunde(1, "Alice", "Smith", "alice@example.com", "123 Main St", 12345, "password", false);
            var kunde2 = new Kunde(2, "Bob", "Jones", "bob@example.com", "456 Elm St", 67890, "password", false);
            var comparer = new Kunde.KundeSortByIdReverse();

            var resultat = comparer.Compare(kunde1, kunde2);

            Assert.IsTrue(resultat > 0); // 2 skal komme før 1
        }

        [TestMethod]
        public void KundeSortByIdReverse_Compare_MedNullVærdier_SkalHåndtereKorrekt()
        {
            // Test for KundeSortByIdReverse med null-værdier
            var kunde1 = new Kunde(1, "Alice", "Smith", "alice@example.com", "123 Main St", 12345, "password", false);
            var comparer = new Kunde.KundeSortByIdReverse();

            var resultat1 = comparer.Compare(kunde1, null);
            var resultat2 = comparer.Compare(null, kunde1);

            Assert.AreEqual(-1, resultat1);
            Assert.AreEqual(1, resultat2);
        }

        [TestMethod]
        public void Kunde_Egenskaber_SkalAcceptereGrænseværdier()
        {
            // Test for at egenskaber accepterer grænseværdier
            var kunde = new Kunde();
            int minKundeId = int.MinValue;
            int maxKundeId = int.MaxValue;
            string maxStreng = new string('A', 255);
            int minPostnr = 0;
            int maxPostnr = 99999;

            kunde.KundeId = minKundeId;
            kunde.Navn = maxStreng;
            kunde.Efternavn = maxStreng;
            kunde.Email = maxStreng;
            kunde.Adresse = maxStreng;
            kunde.Postnr = minPostnr;
            kunde.Kode = maxStreng;

            Assert.AreEqual(minKundeId, kunde.KundeId);
            Assert.AreEqual(maxStreng, kunde.Navn);
            Assert.AreEqual(maxStreng, kunde.Efternavn);
            Assert.AreEqual(maxStreng, kunde.Email);
            Assert.AreEqual(maxStreng, kunde.Adresse);
            Assert.AreEqual(minPostnr, kunde.Postnr);
            Assert.AreEqual(maxStreng, kunde.Kode);

            kunde.Postnr = maxPostnr;

            Assert.AreEqual(maxPostnr, kunde.Postnr);
        }
    }
}
