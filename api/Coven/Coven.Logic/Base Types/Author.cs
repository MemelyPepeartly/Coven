using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.DTO.WorldAnvil
{
    /// <summary>
    /// Information about an author. This is the data type associated with certain returns from Worldanvil
    /// </summary>
    public class Author
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string url { get; set; }
    }
}
