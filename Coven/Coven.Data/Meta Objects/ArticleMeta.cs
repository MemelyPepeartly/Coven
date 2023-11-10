using Coven.Data.Meta_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.Meta_Objects
{
    public class ArticleMeta
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string state { get; set; }
        public bool isWip { get; set; }
        public bool isDraft { get; set; }
        public string templateType { get; set; }
        public int? wordcount { get; set; }
        public int? views { get; set; }
        public int? likes { get; set; }
        public string exerpt { get; set; }
        public string tags { get; set; }
        public bool adultContent { get; set; }
        public WorldAnvilDateDTO lastUpdate { get; set; }
        public string url { get; set; }
        public Author author { get; set; }
        public WorldMeta world { get; set; }
    }
}
