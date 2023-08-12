using shopapp.web.ViewModels;

namespace shopapp.web.Models.Shared;

public class SortInfo
{
    public List<Sort> Sort { get; set; }
    public PageInfo PageInfo { get; set; }
}

public class Sort
{
    public string Name { get; set; }
    public int Value { get; set; }
}
