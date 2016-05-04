using Hera.Persistence.Snapshot;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.MongoDb.Snapshot
{
    public class MongoDbSnapshotStore : ISnapshotStore
    {
        public Persistence.Snapshot.Snapshot Load(string streamId)
        {
            var filter = Builders<MongoDbSnapshot>.Filter.Eq<string>(p => p.StreamId, streamId);
            var sort = Builders<MongoDbSnapshot>.Sort.Descending(p => p.Revision);

            var snapshot = MongoDbHelper.Snapshots.Find(filter).Sort(sort).Limit(1).SingleOrDefault();

            return snapshot;

        }
        public void Save(Persistence.Snapshot.Snapshot snapshot)
        {
            var mongoDbSnapshot = new MongoDbSnapshot(snapshot);

            MongoDbHelper.Snapshots.InsertOne(mongoDbSnapshot);
        }
    }
}
