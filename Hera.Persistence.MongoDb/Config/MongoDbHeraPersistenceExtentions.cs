using Autofac;
using Hera.Persistence;
using Hera.Persistence.EventStore;
using Hera.Persistence.MongoDb.EventStore;
using Hera.Persistence.MongoDb.Snapshot;
using Hera.Persistence.Snapshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera
{
    public static class MongoDbHeraPersistenceExtentions
    {
        public static MongoDbHeraPersistence UsingMongoDbPersistence(this HeraPersistence hera, MongoDbPersistenceOptions options)
        {
            hera.Builder.RegisterInstance(new MongoDbEventStore(options)).As<IEventStore>().SingleInstance();
            hera.Builder.RegisterInstance(new MongoDbSnapshotStore(options)).As<ISnapshotStore>().SingleInstance();

            // TODO: Move on the proper place
            hera.Builder.RegisterType<DefaultCommitNotifier>().As<ICommitNotifier>().SingleInstance();
            hera.Builder.RegisterType<DefaultEventPublisher>().As<IEventPublisher>().SingleInstance();

            return new MongoDbHeraPersistence(hera);
        }
    }
}
