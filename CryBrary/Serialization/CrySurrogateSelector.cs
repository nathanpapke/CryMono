using System.Linq;
using System.Runtime.Serialization;

namespace CryEngine.Serialization
{
    public class CrySurrogateSelector : ISurrogateSelector
    {
        public void ChainSelector(ISurrogateSelector selector)
        {
            throw new System.NotImplementedException();
        }

        public ISurrogateSelector GetNextSelector()
        {
            throw new System.NotImplementedException();
        }

        public ISerializationSurrogate GetSurrogate(System.Type type, StreamingContext context, out ISurrogateSelector selector)
        {
            // Marked with the [Serializable] attribute, use default serialization
            if (type.IsSerializable)
            {
                selector = null;
                return null;
            } 

            // Not marked with [Serializable] but the class (or any of its parents) implement ISerializable
            var serializable = type.GetInterfaces().SingleOrDefault(t => t == typeof(ISerializable)) as ISerializable;
            if (serializable != null)
            {
                selector = this;
                return  new DefaultSerializationSurrogate();
            }

            // No serialization to be found, try our best shot at serialization what we have
            selector = this;
            return new SerializeAllSerializationSurrogate();
        }
    }
}
