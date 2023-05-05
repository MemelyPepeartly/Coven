using Coven.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.DTO.AI
{
    public class UserDTO
    {
        public Guid UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string WorldAnvilUsername { get; set; } = null!;

        public ICollection<WACharacterSetDTO> WacharacterSets { get; set; } = new List<WACharacterSetDTO>();
    }
}
