using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class Waembedding
{
    public Guid EmbeddingId { get; set; }

    public Guid CharacterSetId { get; set; }

    public byte[] Vector { get; set; } = null!;

    public virtual WacharacterSet CharacterSet { get; set; } = null!;
}
