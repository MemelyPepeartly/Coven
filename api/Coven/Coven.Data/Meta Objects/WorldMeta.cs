using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.Meta_Objects
{
    /// <summary>
    /// Baseline world meta type
    /// </summary>
    public class WorldMeta
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string url { get; set; }

    }
    /// <summary>
    /// Another segment type for world meta
    /// </summary>
    public class WorldSegment
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
    }
}
