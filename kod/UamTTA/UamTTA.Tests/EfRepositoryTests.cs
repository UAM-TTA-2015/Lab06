﻿using System.Linq;
using NUnit.Framework;
using UamTTA.Storage;
using System;

namespace UamTTA.Tests
{
    [TestFixture]
    public class EfRepositoryTests
    {
        private EFRepository<Account> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EFRepository<Account>(() => new UamTTAContext());
            foreach (var account in _sut.GetAll())
            {
                _sut.Remove(account);
            }
        }

        [Test]
        public void Persist_Should_Return_Copy_Of_Transient_Object_With_Id_Assigned()
        {
            var someTransientModel = new Account { Id = null, Balance = 10, Name = "Bla" };

            Account result = _sut.Persist(someTransientModel);

            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.Not.SameAs(someTransientModel));
            Assert.That(result.Balance, Is.EqualTo(someTransientModel.Balance));
            Assert.That(result.Name, Is.EqualTo(someTransientModel.Name));
            Assert.That(result.Id.HasValue);
        }

        [Test]
        public void Subsequent_Persist_Calls_Objects_Should_Assign_Different_Id()
        {
            var someTransientModel = new Account { Id = null, Balance = 10, Name = "Bla" };
            var anotherTransientModel = (Account)someTransientModel.Clone();

            Account result1 = _sut.Persist(someTransientModel);
            Account result2 = _sut.Persist(anotherTransientModel);

            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);

            Assert.That(result1, Is.Not.SameAs(someTransientModel));
            Assert.That(result2, Is.Not.SameAs(someTransientModel));
            Assert.That(result1, Is.Not.SameAs(result2));
            Assert.That(result1.Id, Is.Not.Null);
            Assert.That(result2.Id, Is.Not.Null);
            Assert.That(result1.Id, Is.Not.EqualTo(result2.Id));
        }

        [Test]
        public void Persisted_Data_Should_Be_Accesible_By_Id_Via_FindById()
        {
            var someTransientModel = new Account { Id = null, Balance = 10, Name = "Bla" };

            Account persisted = _sut.Persist(someTransientModel);
            Account actual = _sut.FindById(persisted.Id.Value);

            Assert.That(actual.Id, Is.EqualTo(persisted.Id));
            Assert.That(actual.Balance, Is.EqualTo(persisted.Balance));
            Assert.That(actual.Name, Is.EqualTo(persisted.Name));
        }

        [Test]
        public void Persisted_Object_With_Already_Existing_Id_Should_Evict_Previus_Data()
        {
            var someTransientModel = new Account { Id = null, Balance = 10, Name = "Bla" };

            Account persisted = _sut.Persist(someTransientModel);
            var anotherWithSameId = new Account { Id = persisted.Id, Balance = 1121210, Name = "xd^grrr" };
            _sut.Persist(anotherWithSameId);
            Account actual = _sut.FindById(persisted.Id.Value);

            Assert.That(actual.Id, Is.EqualTo(persisted.Id));
            Assert.That(actual.Balance, Is.EqualTo(anotherWithSameId.Balance));
            Assert.That(actual.Name, Is.EqualTo(anotherWithSameId.Name));
        }

        [Test]
        public void FindById_Should_Return_Null_When_Object_Of_Given_Id_Was_Not_Found()
        {
            Account actual = _sut.FindById(4475438);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void FindById_Should_Return_Account_That_We_Are_Looking_For_If_It_Exists()
        {
            var model1 = new Account { Id = null, Balance = 10, Name = "Bla" };
            Account persisted = _sut.Persist(model1);

            Account result = _sut.FindById(persisted.Id.Value);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id.Value, Is.EqualTo(persisted.Id.Value));
            Assert.That(result.Id, Is.EqualTo(persisted.Id));
        }

        [Test]
        public void Remove_Should_Remove_Item_Of_Same_Id_From_Storage()
        {
            var someTransientModel = new Account { Id = null, Balance = 10, Name = "Bla" };

            Account persisted = _sut.Persist(someTransientModel);
            var anotherWithSameId = new Account { Id = persisted.Id };
            _sut.Remove(anotherWithSameId);

            Account actual = _sut.FindById(persisted.Id.Value);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void GetAll_Returns_Empty_Collection_When_Repository_Is_Empty()
        {
            var result = _sut.GetAll();

            Assert.That(result.Any(), Is.False);
            //CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void GetAll_Returns_All_Items()
        {
            var model1 = new Account { Id = null, Balance = 10, Name = "Bla" };
            var model2 = new Account { Id = null, Balance = 12, Name = "BlaBla" };

            _sut.Persist(model1);
            _sut.Persist(model2);
            var result = _sut.GetAll();

            Assert.That(result.Count(), Is.EqualTo(2));
            CollectionAssert.AllItemsAreUnique(result);
        }

        //[Test]
        //public void Take_Should_Throw_Exception_When_Repository_Is_Empty()
        //{
        //    //Arrange
        //    int count = 5;

        //    //Act & Assert
        //    Assert.Throws<ArgumentException>(() => _sut.Take(count));
        //}

        //[Test]
        //public void Take_Should_Throw_Exception_When_There_Is_Less_Objects_Than_Count()
        //{
        //    //Arrange
        //    int count = 5;

        //    var model1 = new Account { Id = null, Balance = 10, Name = "Bla" };
        //    var model2 = new Account { Id = null, Balance = 12, Name = "BlaBla" };

        //    _sut.Persist(model1);
        //    _sut.Persist(model2);

        //    //Act & Assert
        //    Assert.Throws<ArgumentException>(() => _sut.Take(count));
        //}

        //[Test]
        //public void Take_Should_Return_First_Count_Elements_From_Repository()
        //{
        //    //Arrange
        //    int count = 2;

        //    var model1 = new Account { Id = null, Balance = 10, Name = "Bla" };
        //    var model2 = new Account { Id = null, Balance = 12, Name = "BlaBla" };
        //    var model3 = new Account { Id = null, Balance = 40, Name = "BlaBlaBla" };

        //    _sut.Persist(model1);
        //    _sut.Persist(model2);
        //    _sut.Persist(model3);

        //    //Act
        //    var result = _sut.Take(count);

        //    //Assert
        //    //Assert.That(result, Is.Not.Null);
        //    Assert.That(result.Count(), Is.EqualTo(count));
        //    CollectionAssert.Contains(result, _sut.FindById(1));

        //    foreach (var account in result)
        //    {
        //        Assert.That(account, Is.Not.Null);
        //    }
        //}

        //[Test]
        //public void GetByIds_Should_Return_Null_When_There_Is_No_Objects_From_Given_Id_Set()
        //{
        //    int[] ids = new int[] { 1, 2, 3 };

        //    var result = _sut.GetByIds(ids);

        //    Assert.That(result, Is.Null);
        //}

        //[Test]
        //public void GetByIds_Should_Return_All_Objects_From_Given_Id_Set()
        //{
        //    var model1 = new Account { Id = null, Balance = 10, Name = "Bla" };
        //    var model2 = new Account { Id = null, Balance = 12, Name = "BlaBla" };

        //    var persisted1 = _sut.Persist(model1);
        //    var persisted2 = _sut.Persist(model2);

        //    int[] ids = new int[] { persisted1.Id.Value, persisted2.Id.Value };

        //    var result = _sut.GetByIds(ids);

        //    foreach (Account account in result)
        //    {
        //        Assert.That(account, Is.Not.Null);
        //        Assert.That(account.Name, (account.Id.Value == 0) ? Is.EqualTo("Bla") : Is.EqualTo("BlaBla"));
        //    }
        //}

        //[Test]
        //public void GetByIds_Should_Ommit_When_There_Is_No_Object_With_Given_Id()
        //{
        //    var model1 = new Account { Id = null, Balance = 10, Name = "Bla" };
        //    var model2 = new Account { Id = null, Balance = 12, Name = "BlaBla" };

        //    var persisted1 = _sut.Persist(model1);
        //    var persisted2 = _sut.Persist(model2);

        //    int[] ids = new int[] { persisted1.Id.Value, 500, 700, persisted2.Id.Value };

        //    var result = _sut.GetByIds(ids);

        //    Assert.That(result.Count(), Is.EqualTo(2));
        //    foreach (Account account in result)
        //    {
        //        Assert.That(account, Is.Not.Null);
        //    }
        //}
    }
}