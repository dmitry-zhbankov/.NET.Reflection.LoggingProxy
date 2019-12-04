using ImpromptuInterface;
using Logger;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Logging_Proxy
{
    public class DynamicProxy<T> : DynamicObject where T : class
    {
        protected T obj;

        public T CreateInstance(T obj)
        {
            this.obj = obj;
            return this.ActLike<T>();
        }
    }

    public class LoggingProxy<T> : DynamicProxy<T> where T : class
    {
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            var stackTrace = new StackTrace();
            var methodBase = stackTrace.GetFrame(3).GetMethod();
            var parameters = methodBase.GetParameters();
            var parTypes = parameters.Select(x => x.ParameterType).ToArray();

            var methodInfo = typeof(T).GetMethod(binder.Name, parTypes);
            if (methodInfo != null)
            {
                string strTypes = "";
                foreach (var item in parameters)
                {
                    strTypes += $" {item} ";
                }
                Console.WriteLine($"Invoking method {methodInfo.Name} ({strTypes})");
                result = CreateDelegate(obj, methodInfo);
                return true;
            }
            return false;
        }

        static Delegate CreateDelegate(object instance, MethodInfo method)
        {
            var parameters = method.GetParameters()
                       .Select(p => Expression.Parameter(p.ParameterType, p.Name))
                        .ToArray();
            var call = Expression.Call(Expression.Constant(instance), method, parameters);
            return Expression.Lambda(call, parameters).Compile();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var proxy = new LoggingProxy<ILogger>();
            ILogger logger = new Logger.Classes.Logger();
            logger = proxy.CreateInstance(logger);
            logger.Error("error message");
            logger.Error(new Exception("exception message"));
            logger.Info("info message");
            logger.Warning("warning message");
        }
    }
}
