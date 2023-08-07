namespace shopapp.web.Models.Shared;


public class SortInfo
{
    public List<Sort> Sorts { get; set; }
    public int SelectedPage { get; set; }
    public string SelectedPrice { get; set; }
    public string[] SelectedCategory { get; set; }
    public int SelectedSort { get; set; }
}
public class Sort
{
    public string Name { get; set; }
    public int Value { get; set; }
}
