using Coven.Logic.Base_Types;
using Coven.Logic.Meta_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.DTO.WorldAnvil
{
    /// <summary>
    /// Article object that comes as part of the response from getting a world
    /// </summary>
    public class WorldAnvilUserArticles
    {
        public WorldMeta world { get; set; }
        public string term { get; set; }
        public string offset { get; set; }
        public string order_by { get; set; }
        public string trajectory { get; set; }
        public List<UserWorldArticleMeta> articles { get; set; }
    }
}
