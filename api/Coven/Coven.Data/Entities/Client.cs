using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class Client
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Character> Characters { get; } = new List<Character>();
}
