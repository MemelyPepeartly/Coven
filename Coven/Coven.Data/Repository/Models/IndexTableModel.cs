using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.Repository.Models
{
    public class IndexTableModel
    {
        public string content { get; set; }
        public string articleTitle { get; set; }
        public string worldAnvilArticleType { get; set; }
        public string author { get; set; }
        public Guid articleId { get; set; }
        public Guid worldId { get; set; }
    }
}
