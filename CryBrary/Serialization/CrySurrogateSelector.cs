using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CryEngine.Serialization
{
    public class CrySurrogateSelector : ISurrogateSelector
    {
        private ISerializationSurrogate _serializationSurrogate;
        public CrySurrogateSelector()
        {
            _serializationSurrogate = new CrySerializationSurrogate();
        }

        public void ChainSelector(ISurrogateSelector selector)
        {
        }

        public ISurrogateSelector GetNextSelector()
        {
            return null;
        }

        public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
        {
            // If the type is marked as serializable, use the default serialization process
            if (type.IsSerializable)
            {
                selector = null;
                return null;
            }
            else // Otherwise, use the Cry Serialization (best shot at serialization)
            {
                selector = this;
                return _serializationSurrogate;
            }
        }
    }
}
