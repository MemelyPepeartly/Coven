using Coven.Logic.DTO.WorldAnvil;
using Coven.Logic.Meta_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.Base_Types
{
    public class Article
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string state { get; set; }
        public bool is_wip { get; set; }
        public bool is_draft { get; set; }
        public string template_type { get; set; }
        public int wordcount { get; set; }
        public int views { get; set; }
        public int likes { get; set; }
        public string exerpt { get; set; }
        public string tags { get; set; }
        public bool adult_content { get; set; }
        public string last_update { get; set; }
        public string url { get; set; }
        public Author author { get; set; }
        public WorldMeta world { get; set; }
    }
}
