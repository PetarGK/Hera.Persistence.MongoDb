using Hera.Persistence.EventStore;
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
            throw new NotImplementedException();
        }
        public EventStream Load(string streamId)
        {
            throw new NotImplementedException();
        }
        public EventStream Load(string streamId, int skipRevision)
        {
            throw new NotImplementedException();
        }
    }
}
