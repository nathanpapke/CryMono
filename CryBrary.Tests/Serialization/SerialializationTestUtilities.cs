using CryEngine.Native;
using Moq;

namespace CryBrary.Tests.Serialization
{
    static class SerialializationTestUtilities
    {
        public static void MockSerializationDependencies()
        {
            NativeCVarMethods.Instance = new Mock<INativeCVarMethods>().Object;
            NativeEntityMethods.Instance = new Mock<INativeEntityMethods>().Object;
            NativeLoggingMethods.Instance = new Mock<INativeLoggingMethods>().Object;
        }
    }
}
