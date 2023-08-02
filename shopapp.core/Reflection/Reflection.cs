using System.Reflection;

namespace shopapp.core.Reflection
{
    public static class Reflection
    {
        public static string CreateCacheKey(Type type,string methodName,params object[]? parametres)
        {
            
            var methodNameValue = string.Format("{0} | {1} | {2}",
                type.Namespace,
                type.Name,
                type.GetMethod(methodName)?.Name
            );


            return string.Format("{0}({1})", methodNameValue, string.Join(",", parametres ?? new object[0]));
        }
    }
}
