using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Hera.ExampleModel;
using Hera.DomainModeling.Repository;
using Hera.Serialization;
using System.IO;
using Hera.Persistence.Snapshot;
using Hera.DomainModeling.AggregareRoot;

namespace Hera.Persistence.MongoDb.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private OrderId _orderId = OrderId.NewId;
        private CustomerId _customerId = new CustomerId(Guid.NewGuid());
        private IContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new ContainerBuilder();

            Hera.Init(builder)
                .SetupPersistence()
                .UsingMongoDbPersistence(new MongoDbPersistenceOptions()
                {
                    DatabaseName = "EventStore"
                })
                .UsingBinarySerialization();

            _container = builder.Build();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _container.Dispose();
        }


        [TestMethod]
        public void SaveAggregate()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var aggregateRepository = scope.Resolve<IAggregateRepository>();
                var order = CreateOrder();

                aggregateRepository.Save(order);
            }
        }

        [TestMethod]
        public void LoadAggregate()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var aggregateRepository = scope.Resolve<IAggregateRepository>();
                var order = aggregateRepository.Load<Order>(new OrderId(Guid.Parse("8fea6a04-e614-45b6-9b07-fbb08dbf5ed4")));

                //order.AddProduct(new ProductId(Guid.NewGuid()), new Price(10.0m), 2);

                //aggregateRepository.Save(order);
            }
        }

        [TestMethod]
        public void CreateSnapshot()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var snapshotManager = scope.Resolve<ISnapshotManager>();

                var order = CreateOrder();

                order.AddProduct(new ProductId(Guid.NewGuid()), new Price(30.0m), 3);

                snapshotManager.CreateSnapshot(order);

                var order2 = snapshotManager.RestoreAggregate<Order>(_orderId);
            }
        }

        [TestMethod]
        public void LoadSnapshot()
        {

        }

        [TestMethod]
        public void SerializeAggregate()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var serializer = scope.Resolve<ISerialize>();
                var order = CreateOrder();

                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, order);

                    var serializedOrder = stream.ToArray();

                    using (var stream2 = new MemoryStream(serializedOrder))
                    {
                        var order2 = serializer.Deserialize<Order>(stream2);

                        order2.AddProduct(new ProductId(Guid.NewGuid()), new Price(20.0m), 2);

                        using (var stream3 = new MemoryStream())
                        {
                            serializer.Serialize(stream3, order2);

                            var serializedOrder2 = stream3.ToArray();

                            using (var stream4 = new MemoryStream(serializedOrder2))
                            {
                                var order3 = serializer.Deserialize<Order>(stream4);

                            }
                        }
                    }
                }
            }
        }


        private Order CreateOrder()
        {
            return new Order(_customerId, _orderId);
        }

    }
}
