using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class PineconeVectorMetadatum
{
    public Guid EntryId { get; set; }

    public Guid WorldId { get; set; }

    public Guid ArticleId { get; set; }

    public string CharacterString { get; set; } = null!;

    public virtual World World { get; set; } = null!;
}
