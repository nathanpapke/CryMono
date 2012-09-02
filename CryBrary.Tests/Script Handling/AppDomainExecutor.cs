using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryBrary.Tests.ScriptHandling
{
    public class AppDomainExecutor : MarshalByRefObject
    {
        public void Execute(Action action)
        {
            action();
        }

        public void Execute<T>(Action<T> action, T arg)
        {
            action(arg);
        }

        public TResult Func<T, TResult>(Func<T, TResult> func, T arg)
        {
            return func(arg);
        }
    }
}
