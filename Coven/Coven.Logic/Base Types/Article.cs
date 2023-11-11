using Coven.Data.Meta_Objects;
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
        public bool isWip { get; set; }
        public bool isDraft { get; set; }
        public string state { get; set; }
        public string passcode { get; set; }
        public int wordcount { get; set; }
        public WorldAnvilDateDTO creationDate { get; set; }
        public WorldAnvilDateDTO updateDate { get; set; }
        public WorldAnvilDateDTO publicationDate { get; set; }
        public WorldAnvilDateDTO notificationDate { get; set; }
        public string tags { get; set; }
        public string url { get; set; }
        public Category category { get; set; }
        /// <summary>
        /// Username may not populate
        /// </summary>
        public Author author { get; set; }
        public WorldMeta world { get; set; }
        public string content { get; set; }
        public string contentParsed { get; set; }
        public object sections { get; set; }
        public object relations  { get; set; }
        public string fullRender { get; set; }

    }
}
