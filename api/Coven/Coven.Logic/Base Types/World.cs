using Coven.Logic.DTO.WorldAnvil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.Base_Types
{
    public partial class World
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string locale { get; set; }
        public string description { get; set; }
        public string descriptionParsed { get; set; }
        public string displayCss { get; set; }
        public int theme { get; set; }
        public string tags { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public Author author { get; set; }
    }
}
