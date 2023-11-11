using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class WorldContent
{
    public Guid WorldContentId { get; set; }

    public Guid WorldId { get; set; }

    public Guid ArticleId { get; set; }

    public string? Content { get; set; }

    public virtual World World { get; set; } = null!;
}
