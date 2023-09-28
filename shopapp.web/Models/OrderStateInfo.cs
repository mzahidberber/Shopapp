namespace shopapp.web.Models;

public static class OrderStateInfo
{
    public static Dictionary<int, string> OrderState = new Dictionary<int, string>
    {
        {0,"Has been taken" },
        {1,"Getting ready" },
        {2,"Has been shipped" },
        {3,"In Cargo" },
        {4,"Has delivered" }
    };
}
