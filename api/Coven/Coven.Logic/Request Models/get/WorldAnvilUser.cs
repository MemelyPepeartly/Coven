using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.Request_Models.Get
{
    public class WorldAnvilUser
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string url { get; set; }
    }
}
