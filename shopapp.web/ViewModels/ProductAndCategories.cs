﻿using shopapp.core.DTOs.Concrete;

namespace shopapp.web.ViewModels
{
    public class ProductAndCategories
    {
        public PageInfo PageInfo { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }

    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems/ItemsPerPage);
        }
    }
}
