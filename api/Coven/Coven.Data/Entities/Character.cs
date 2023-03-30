using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class Character
{
    public Guid CharacterId { get; set; }

    public string CharacterName { get; set; } = null!;

    public Guid UserId { get; set; }

    public virtual Client User { get; set; } = null!;
}
