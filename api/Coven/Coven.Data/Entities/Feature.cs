using System;
using System.Collections.Generic;

namespace Tardigrade.Data.Entities;

public partial class Feature
{
    public Guid FeatureId { get; set; }

    public string FeatureName { get; set; } = null!;

    public Guid FeatureCategoryId { get; set; }

    public virtual FeatureCategory FeatureCategory { get; set; } = null!;
}
