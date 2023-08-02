using Castle.DynamicProxy;
using System.Reflection;

namespace shopapp.core.Aspects
{
    public class AspectSelector: IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes =
                type.GetCustomAttributes<BaseAspect>(true)
                .ToList();

            var methodAttributes =
                type.GetMethod(method.Name)?
                .GetCustomAttributes<BaseAspect>(true);

            if (methodAttributes != null)
                classAttributes.AddRange(methodAttributes);

            return classAttributes
                .OrderBy(x => x.Priority)
                .ToArray();
        }
    }
}
