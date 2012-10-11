using System;
using System.Runtime.Serialization;

namespace CryEngine.Serialization
{
    public class CrySurrogateSelector : ISurrogateSelector
    {
        private readonly ISerializationSurrogate _serializationSurrogate;
        public CrySurrogateSelector()
        {
            _serializationSurrogate = FormatterServices.GetSurrogateForCyclicalReference(new CrySerializationSurrogate());
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
            selector = this;
            return _serializationSurrogate;
        }
    }
}
