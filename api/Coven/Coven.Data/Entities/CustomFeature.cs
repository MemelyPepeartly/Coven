using System;
using System.Collections.Generic;

namespace Coven.Data.Entities;

public partial class CustomFeature
{
    public Guid CustomFeatureId { get; set; }

    public string CustomFeatureName { get; set; } = null!;

    public Guid FeatureCategoryId { get; set; }

    public virtual FeatureCategory FeatureCategory { get; set; } = null!;
}
