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
        public string template { get; set; }
        public bool is_wip { get; set; }
        public bool is_draft { get; set; }
        public string state { get; set; }
        public string passcode { get; set; }
        public int wordcount { get; set; }
        public WorldAnvilDateDTO creation_date { get; set; }
        public WorldAnvilDateDTO update_date { get; set; }
        public WorldAnvilDateDTO publication_date { get; set; }
        public WorldAnvilDateDTO notification_date { get; set; }
        public string tags { get; set; }
        public string url { get; set; }
        public Category category { get; set; }
        public Author author { get; set; }
        public WorldMeta world { get; set; }
        public string content { get; set; }
        public string content_parsed { get; set; }
        public object sections { get; set; }
        public object relations  { get; set; }
        public string full_render { get; set; }

    }
    public class WorldAnvilDateDTO
    {
        public DateTime date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }

    }
}
