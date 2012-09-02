using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryBrary.Tests.ScriptHandling
{
    public class CrossAppDomainStorage : MarshalByRefObject
    {
        private readonly Dictionary<string, object> _values;
        public Dictionary<string,object> Values
        {
            get { return _values; }
        }

        public CrossAppDomainStorage()
        {
           _values = new Dictionary<string, object>();
        }
    }
}
