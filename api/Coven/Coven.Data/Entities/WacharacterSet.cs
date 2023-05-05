using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class WacharacterSet
{
    public Guid CharacterSetId { get; set; }

    public Guid UserId { get; set; }

    public string CharacterSet { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Waembedding> Waembeddings { get; set; } = new List<Waembedding>();
}
