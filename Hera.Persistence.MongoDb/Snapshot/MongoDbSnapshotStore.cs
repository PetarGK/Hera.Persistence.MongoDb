using Hera.Persistence.Snapshot;
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
            throw new NotImplementedException();
        }
        public void Save(Persistence.Snapshot.Snapshot snapshot)
        {
            throw new NotImplementedException();
        }
    }
}
