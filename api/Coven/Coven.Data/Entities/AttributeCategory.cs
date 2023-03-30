using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class AttributeCategory
{
    public Guid AttributeCategoryId { get; set; }

    public string AttributeCategoryName { get; set; } = null!;

    public string AttributeCategoryDescription { get; set; } = null!;

    public virtual ICollection<Attribute> Attributes { get; } = new List<Attribute>();
}
