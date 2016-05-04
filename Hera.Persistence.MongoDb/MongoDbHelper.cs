using Hera.Persistence.MongoDb.EventStore;
using Hera.Persistence.MongoDb.Snapshot;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.MongoDb
{
    internal static class MongoDbHelper
    {
        private static IMongoCollection<MongoDbCommitStream> _commits;
        private static IMongoCollection<MongoDbSnapshot> _snapshots;

        static MongoDbHelper()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("EventStore");
            _commits = database.GetCollection<MongoDbCommitStream>("Commits");
            _snapshots = database.GetCollection<MongoDbSnapshot>("Snapshots");
        }

        public static IMongoCollection<MongoDbCommitStream> Commits
        {
            get { return _commits; }
        }
        public static IMongoCollection<MongoDbSnapshot> Snapshots
        {
            get { return _snapshots; }
        }
    }
}
