using CryEngine.Compilers.NET;
using CryEngine.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NETFramework.Tests
{
    public class CompilerTests : IDisposable
    {
        public CompilerTests()
        {
            // Init
        }

        [Fact]
        public void Construct_Default_Successfully()
        {
            // Arrange
            NETCompiler compiler;

            // Act
            compiler = new NETCompiler();

            // Assert
            Assert.NotNull(compiler);
            Assert.IsAssignableFrom<ScriptCompiler>(compiler);
        }


        [Fact]
        public void GetCryScriptsFromAssembly_NullAssembly_ThrowsException()
        {
            // Arrange
            var compiler = new NETCompiler();

            // Act
            Assert.ThrowsDelegate action = () => compiler.GetCryScriptsFromAssembly(null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.NotNull(exception);
            Assert.Equal(exception.ParamName, "assembly");
        }

        public void Dispose()
        {
            // Cleanup
        }
    }
}
