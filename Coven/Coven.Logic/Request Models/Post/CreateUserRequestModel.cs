using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.Request_Models.Post
{
    public class CreateUserRequestModel
    {
        public string username { get; set; }
        public string worldAnvilUsername { get; set; }
        public string email { get; set; }
    }
}
