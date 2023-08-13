﻿using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopapp.core.DTOs.Concrete;

public class SubCategoryDTO:IDTO
{
	public SubCategoryDTO()
	{
		this.Brands = new List<BrandDTO>();
		this.Products = new List<ProductDTO>();
		this.SubCategoryFeatures = new List<SubCategoryFeatureDTO>();
	}
	public int Id { get; set; }
	public string Name { get; set; }
	public string Url { get; set; }

	public int CategoryId { get; set; }
	public CategoryDTO Category { get; set; }

	public List<ProductDTO> Products { get; set; }
	public List<BrandDTO> Brands { get; set; }
	public List<SubCategoryFeatureDTO> SubCategoryFeatures { get; set; }
}
