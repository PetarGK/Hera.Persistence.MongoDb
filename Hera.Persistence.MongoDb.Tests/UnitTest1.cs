using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;

namespace Hera.Persistence.MongoDb.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new ContainerBuilder();

            Hera.Init(builder)
                .SetupPersistence()
                .UsingMongoDbPersistence(new MongoDbPersistenceOptions() {

                });

            _container = builder.Build();
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
