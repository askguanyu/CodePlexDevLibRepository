using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevLib.Repository;
using DevLib.Repository.EntityFramework;
using System.Diagnostics;
using System.IO;

namespace DevLib.Repository.EntityFramework.UnitTest
{
    [TestClass]
    public class Given_Repository
    {
        private EntityFrameworkRepository<TestEntityA> _testEntityARepo;
        private AutoIncrementId _id = new AutoIncrementId(Stopwatch.GetTimestamp(), 1);

        public Given_Repository()
        {
            var connectionString = File.ReadAllText("ConnectionString.txt");
            this._testEntityARepo = new EntityFrameworkRepository<TestEntityA>(connectionString);
            this._testEntityARepo.Log = msg => Debug.WriteLine(msg);
        }

        [TestMethod]
        public void When_Insert()
        {
            var a = new TestEntityA() { Id = _id.Next().ToString() };

            var z = this._testEntityARepo.Insert(a);

        }

        [TestMethod]
        public void When_DeleteAll()
        {
            //var a = new TestEntityA() { Id = _id.Next().ToString() };

            var z = this._testEntityARepo.DeleteAll();

        }
    }
}
