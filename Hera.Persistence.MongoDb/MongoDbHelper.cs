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
        private static bool _isInitialized;
        private static object _lock;
        private static IMongoCollection<MongoDbCommitStream> _commits;
        private static IMongoCollection<MongoDbSnapshot> _snapshots;

        static MongoDbHelper()
        {
            _isInitialized = false;
            _lock = new object();
        }

        public static void Init(MongoDbPersistenceOptions options)
        {
            lock(_lock)
            {
                if (!_isInitialized)
                {
                    var client = new MongoClient();
                    var database = client.GetDatabase(options.DatabaseName);
                    _commits = database.GetCollection<MongoDbCommitStream>("Commits");
                    _snapshots = database.GetCollection<MongoDbSnapshot>("Snapshots");

                    _isInitialized = true;
                }
            }
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
