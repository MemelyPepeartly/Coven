using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class World
{
    public Guid WorldId { get; set; }

    public Guid UserId { get; set; }

    public string WorldName { get; set; } = null!;

    public virtual ICollection<PineconeVectorMetadatum> PineconeVectorMetadata { get; set; } = new List<PineconeVectorMetadatum>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<WorldContent> WorldContents { get; set; } = new List<WorldContent>();
}
