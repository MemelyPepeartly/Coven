using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string WorldAnvilUsername { get; set; } = null!;

    public virtual ICollection<WacharacterSet> WacharacterSets { get; set; } = new List<WacharacterSet>();

    public virtual ICollection<Wasnippet> Wasnippets { get; set; } = new List<Wasnippet>();
}
