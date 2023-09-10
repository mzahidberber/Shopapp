using Castle.DynamicProxy;
using NLog;

namespace shopapp.core.Aspects.Logging
{
    public class LogAspect : Aspect
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public override void OnBefore(IInvocation invocation)
        {
            var message = string.Format("{0} | {1} | {2} | {3}",
                invocation.Method.ReflectedType?.Namespace,
                invocation.Method.ReflectedType?.Name,
                invocation.Method.Name,
                GetParams(invocation)
            );
            //_logger.Info(message);
        }

        public override void OnException(IInvocation invocation, Exception ex)
        {
            var message = string.Format("{0} | {1} | {2} | {3}",
            invocation.Method.ReflectedType?.Namespace,
            invocation.Method.ReflectedType?.Name,
            invocation.Method.Name,
            ex.Message
            );

            //_logger.Info(message);
        }

        private string GetParams(IInvocation IInvocation)
        {
            var parametres = "";

            foreach (var argument in IInvocation.Arguments.ToList())
            {
                if (argument != null)
                {
                    parametres += $"{argument.GetType().Name}:{argument.ToString()} | ";

                    if (argument.GetType().GetProperties().Length > 0 && argument.GetType().Name != "String")
                    {
                        if (argument.GetType() != typeof(List<int>))
                        {
                            foreach (var prop in argument.GetType().GetProperties())
                            {
                                parametres += $"{prop.Name}:{prop.GetValue(argument)} ";
                            };
                        }

                    }
                }
                else
                {
                    parametres += $"{argument?.GetType().Name}:<Null>";
                }
            }
            return parametres;
        }

        //private string? GetUserInfo(IInvocation invocation)
        //{
        //    var userName = new object();
        //    var userId = new object();
        //    if (invocation.Instance is ILog)
        //    {
        //        userName = args.Instance.GetType().GetMethod("GetUserName")?.Invoke(args.Instance, new object[] { });
        //        userId = args.Instance.GetType().GetMethod("GetUserId")?.Invoke(args.Instance, new object[] { });
        //        return $"{userId} -- {userName}";
        //    }
        //    return null;
        //}
    }
}
