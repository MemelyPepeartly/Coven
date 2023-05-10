using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.Meta_Objects
{
    public class WorldDTO
    {
        public Guid WorldId { get; set; }

        public Guid UserId { get; set; }

        public string WorldName { get; set; } = null!;
    }
}
