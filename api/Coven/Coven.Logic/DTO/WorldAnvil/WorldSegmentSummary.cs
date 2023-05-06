using Coven.Logic.Meta_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.DTO.WorldAnvil
{
    public class WorldSegmentSummary
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public List<WorldSegment> worlds { get; set; }
    }
}
