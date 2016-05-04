using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.MongoDb.Snapshot
{
    public class MongoDbSnapshot : Persistence.Snapshot.Snapshot
    {
        public ObjectId Id { get; set; }

        public MongoDbSnapshot(Persistence.Snapshot.Snapshot snapshot) 
            : base(snapshot.StreamId, snapshot.Revision, snapshot.Payload)
        {

        }
    }
}
