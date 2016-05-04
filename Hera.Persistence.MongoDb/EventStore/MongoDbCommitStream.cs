using Hera.Persistence.EventStore;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.MongoDb.EventStore
{
    public class MongoDbCommitStream : CommitStream
    {
        public ObjectId Id { get; set; }

        public MongoDbCommitStream(CommitStream commit) 
            : base(commit.StreamId, commit.Revision, commit.Payload)
        {

        }
    }
}
