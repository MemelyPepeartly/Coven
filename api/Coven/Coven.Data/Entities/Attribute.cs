using System;
using System.Collections.Generic;

namespace Tardigrade.Data.Entities;

public partial class Attribute
{
    public Guid AttributeId { get; set; }

    public string AttributeName { get; set; } = null!;

    public string AttributeValue { get; set; } = null!;

    public Guid AttributeCategoryId { get; set; }

    public virtual AttributeCategory AttributeCategory { get; set; } = null!;
}
