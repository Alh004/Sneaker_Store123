using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sneaker_Store.Model;

namespace Sneaker_Store.Model.Tests
{
    [TestClass]
    public class KundeTests
    {
        private Kunde _defaultKunde;
        private Kunde _parameterizedKunde;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialiser standard kunde
            _defaultKunde = new Kunde();

            // Initialiser parameteriseret kunde
            _parameterizedKunde = new Kunde(1, "John", "Doe", "john.doe@example.com", "123 Main St", "Copenhagen", 1234, "password123", true);
        }

        [TestMethod]
        public void Kunde_DefaultConstructor_SetsPropertiesToDefaultValues()
        {
            // Test standard konstruktør
            Assert.AreEqual(0, _defaultKunde.KundeId);
            Assert.AreEqual("", _defaultKunde.Navn);
            Assert.AreEqual("", _defaultKunde.Efternavn);
            Assert.AreEqual("", _defaultKunde.Email);
            Assert.AreEqual("", _defaultKunde.Adresse);
            Assert.AreEqual("", _defaultKunde.By);
            Assert.AreEqual(0, _defaultKunde.Postnr);
            Assert.AreEqual("", _defaultKunde.Kode);
            Assert.AreEqual(false, _defaultKunde.Admin);
        }

        [TestMethod]
        public void Kunde_ParameterizedConstructor_SetsPropertiesCorrectly()
        {
            // Test parameteriseret konstruktør
            Assert.AreEqual(1, _parameterizedKunde.KundeId);
            Assert.AreEqual("John", _parameterizedKunde.Navn);
            Assert.AreEqual("Doe", _parameterizedKunde.Efternavn);
            Assert.AreEqual("john.doe@example.com", _parameterizedKunde.Email);
            Assert.AreEqual("123 Main St", _parameterizedKunde.Adresse);
            Assert.AreEqual("Copenhagen", _parameterizedKunde.By);
            Assert.AreEqual(1234, _parameterizedKunde.Postnr);
            Assert.AreEqual("password123", _parameterizedKunde.Kode);
            Assert.AreEqual(true, _parameterizedKunde.Admin);
        }

        [TestMethod]
        public void Kunde_SetProperties_CorrectValuesAreAssigned()
        {
            // Test indstilling af egenskaber
            _defaultKunde.KundeId = 2;
            _defaultKunde.Navn = "Jane";
            _defaultKunde.Efternavn = "Smith";
            _defaultKunde.Email = "jane.smith@example.com";
            _defaultKunde.Adresse = "456 Another St";
            _defaultKunde.By = "Aarhus";
            _defaultKunde.Postnr = 5678;
            _defaultKunde.Kode = "password456";
            _defaultKunde.Admin = false;

            Assert.AreEqual(2, _defaultKunde.KundeId);
            Assert.AreEqual("Jane", _defaultKunde.Navn);
            Assert.AreEqual("Smith", _defaultKunde.Efternavn);
            Assert.AreEqual("jane.smith@example.com", _defaultKunde.Email);
            Assert.AreEqual("456 Another St", _defaultKunde.Adresse);
            Assert.AreEqual("Aarhus", _defaultKunde.By);
            Assert.AreEqual(5678, _defaultKunde.Postnr);
            Assert.AreEqual("password456", _defaultKunde.Kode);
            Assert.AreEqual(false, _defaultKunde.Admin);
        }

        [TestMethod]
        public void Kunde_Grænseværdier_Test()
        {
            // Test grænseværdier for Postnr
            _defaultKunde.Postnr = 0;
            Assert.AreEqual(0, _defaultKunde.Postnr);

            _defaultKunde.Postnr = 9999;
            Assert.AreEqual(9999, _defaultKunde.Postnr);

            // Test grænseværdier for boolean
            _defaultKunde.Admin = true;
            Assert.AreEqual(true, _defaultKunde.Admin);

            _defaultKunde.Admin = false;
            Assert.AreEqual(false, _defaultKunde.Admin);
        }

        [TestMethod]
        public void Kunde_ToString_ReturnsCorrectString()
        {
            // Test ToString metoden
            string expectedString = "KundeId: 1, Navn: John, Efternavn: Doe, Email: john.doe@example.com, Adresse: 123 Main St, By: Copenhagen, Postnr: 1234, Kode: password123, Admin: True";
            string actualString = _parameterizedKunde.ToString();
            
            // Udskrivning af den faktiske streng for diagnosticering
            Console.WriteLine($"Forventet: {expectedString}");
            Console.WriteLine($"Faktisk: {actualString}");
            
            Assert.AreEqual(expectedString, actualString);
        }
    }
}
