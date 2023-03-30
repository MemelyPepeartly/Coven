using System;
using System.Collections.Generic;

namespace Tardigrade.Data.Entities;

public partial class FeatureCategory
{
    public Guid FeatureCategoryId { get; set; }

    public string FeatureCategoryName { get; set; } = null!;

    public string FeatureCategoryDescription { get; set; } = null!;

    public virtual ICollection<CustomFeature> CustomFeatures { get; } = new List<CustomFeature>();

    public virtual ICollection<Feature> Features { get; } = new List<Feature>();
}
