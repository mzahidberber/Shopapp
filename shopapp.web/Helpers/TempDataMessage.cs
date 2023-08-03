using Microsoft.AspNetCore.Mvc.ViewFeatures;
using shopapp.web.Extensions;
using shopapp.web.Models;

namespace shopapp.web.Helpers;

public static class TempDataMessage
{
    public static void CreateMessage(ITempDataDictionary tempData,string key, string alertType = "success", string? title=null,string? message=null)
    {
        tempData.Put(key, new AlertMessage()
        {
            Title=title ?? "",
            Message = message?? "",
            AlertType = alertType
        });
    }
}
