using Hera.Persistence.EventStore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.MongoDb.EventStore
{
    public class MongoDbEventStore : IEventStore
    {
        public void Append(CommitStream commitStream)
        {
            var commit = new MongoDbCommitStream(commitStream);

            MongoDbHelper.Commits.InsertOne(commit);
        }
        public EventStream Load(string streamId)
        {
            var commits = MongoDbHelper.Commits.Find(Builders<MongoDbCommitStream>.Filter.Eq<string>(p => p.StreamId, streamId)).ToList();

            return new EventStream(commits);
        }
        public EventStream Load(string streamId, int skipRevision)
        {
            var filter1 = Builders<MongoDbCommitStream>.Filter.Eq<string>(p => p.StreamId, streamId);
            var filter2 = Builders<MongoDbCommitStream>.Filter.Lt<int>(p => p.Revision, skipRevision);
            var filter = filter1 & filter2;

            var commits = MongoDbHelper.Commits.Find(filter).ToList();

            return new EventStream(commits);
        }
    }
}
