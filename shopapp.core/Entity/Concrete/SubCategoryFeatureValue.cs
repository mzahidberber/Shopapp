﻿namespace shopapp.core.Entity.Concrete;

public class SubCategoryFeatureValue
{
    public int Id { get; set; }
    public string Value { get; set; }

    public int SubCategoryFeatureId { get; set; }
    public SubCategoryFeature SubCategoryFeature { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

}
