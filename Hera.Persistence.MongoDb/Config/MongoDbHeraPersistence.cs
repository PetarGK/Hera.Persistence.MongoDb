using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera
{
    public class MongoDbHeraPersistence : HeraPersistence
    {
        public MongoDbHeraPersistence(Hera inner) : base(inner)
        {

        }
    }
}
