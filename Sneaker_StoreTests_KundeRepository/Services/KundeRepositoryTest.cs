using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using System;

namespace Sneaker_Store.Services.Tests
{
    [TestClass]
    public class KundeRepositoryTests
    {
        private KundeRepository _kundeRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialiser kunde repository med testdata
        }

        [TestMethod]
        public void AddKunde_ValidPerson_AddsPersonToList()
        {
            var newKunde = new Kunde(3, "new", "person", "new@3.dk", "vej", 2450, "test3", false);
            _kundeRepository.AddKunde(newKunde);
            var result = _kundeRepository.GetAll();
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("new", result[2].Navn);
        }

        [TestMethod]
        public void AddKunde_NullPerson_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => _kundeRepository.AddKunde(null));
            Assert.AreEqual("kunde", exception.ParamName);
        }

        [TestMethod]
        public void RemoveKunde_ExistingPerson_RemovesPersonFromList()
        {
            var kundeToRemove = _kundeRepository.GetAll()[0];
            _kundeRepository.RemoveKunde(kundeToRemove);
            var result = _kundeRepository.GetAll();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("dani", result[0].Navn);
        }

        [TestMethod]
        public void RemoveKunde_NonExistingPerson_ListRemainsUnchanged()
        {
            var nonExistingKunde = new Kunde(999, "non", "existing", "non@999.dk", "vej", 2450, "test999", false);
            _kundeRepository.RemoveKunde(nonExistingKunde);
            var result = _kundeRepository.GetAll();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAll_EmptyList_ReturnsEmptyList()
        {
            var repo = new KundeRepository();
            var result = repo.GetAll();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetAll_MultiplePersonsInList_ReturnsListWithMultiplePersons()
        {
            var result = _kundeRepository.GetAll();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("ali", result[0].Navn);
            Assert.AreEqual("dani", result[1].Navn);
        }

        [TestMethod]
        public void CheckKunde_ValidCredentials_ReturnsLoginResultAndSetsKundeLoggedIn()
        {
            var result = _kundeRepository.CheckKunde("ali@1.dk", "test");
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsAdmin);
            Assert.AreEqual("ali", _kundeRepository.KundeLoggedIn.Navn);
        }

        [TestMethod]
        public void CheckKunde_InvalidCredentials_ReturnsNullAndKundeLoggedInIsNull()
        {
            var result = _kundeRepository.CheckKunde("invalid@1.dk", "wrong");
            Assert.IsNull(result);
            Assert.IsNull(_kundeRepository.KundeLoggedIn);
        }

        [TestMethod]
        public void LogoutKunde_LoggedInUser_LogsOutUser()
        {
            _kundeRepository.CheckKunde("ali@1.dk", "test");
            _kundeRepository.LogoutKunde();
            Assert.IsNull(_kundeRepository.KundeLoggedIn);
        }
    }
}
