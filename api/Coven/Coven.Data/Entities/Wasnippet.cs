using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class Wasnippet
{
    public Guid SnippetId { get; set; }

    public Guid UserId { get; set; }

    public Guid WorldAnvilArticleId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
