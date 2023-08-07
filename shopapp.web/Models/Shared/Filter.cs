using shopapp.web.Models.Entity;

namespace shopapp.web.Models.Shared;


public class FilterInfo
{
    public List<CategoryModel> Categories { get; set; }
    public FilterPrice FilterPrice { get; set; }
    public Filter FilterSort { get; set; }
    public string SelectedPrice { get; set; }
    public int SelectedSort { get; set; }
    public string[] SelectedCategories { get; set; }
}

public class FilterPrice
{
    public int LeastPrice { get; set; }
    public int MostPrice { get; set; }

    public int CalculateInvertalPrice()
    {
        return (MostPrice - LeastPrice) / 4;
    }
}


public class Filter
{
    public string Name { get; set; }
}
