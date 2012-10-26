using CryEngine.Compilers.NET;
using CryEngine.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Reflection;
using NETFramework.Tests.Entities;

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
            NetCompiler compiler;

            // Act
            compiler = new NetCompiler();

            // Assert
            Assert.NotNull(compiler);
            Assert.IsAssignableFrom<ScriptCompiler>(compiler);
        }


        [Fact]
        public void GetCryScriptsFromAssembly_NullAssembly_ThrowsException()
        {
            // Arrange
            var compiler = new NetCompiler();

            // Act
            Assert.ThrowsDelegate action = () => compiler.GetCryScriptsFromAssembly(null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.NotNull(exception);
            Assert.Equal(exception.ParamName, "assembly");
        }

        [Fact]
        public void GetCryScriptsFromAssembly_TestAssembly_ContainsCorrectEntityScript()
        {
            // Arrange
            var compiler = new NetCompiler();

            // Act
            var foundScripts = compiler.GetCryScriptsFromAssembly(Assembly.GetExecutingAssembly());

            // Assert
            Assert.NotNull(foundScripts);
            Assert.NotEmpty(foundScripts);

            var entityScript = foundScripts.Single(c => c.Type == typeof (SampleEntity));
            Assert.NotEmpty(entityScript.RegistrationParams);
            Assert.Equal(entityScript.RegistrationParams.Count, 1);
            Assert.Equal(entityScript.ScriptType & ScriptType.Entity, ScriptType.Entity);
            Assert.IsType<EntityRegistrationParams>(entityScript.RegistrationParams.First());

            var entityRegistrationParams = (EntityRegistrationParams) entityScript.RegistrationParams.First();
            Assert.Equal(entityRegistrationParams.Category, "SampleCategory");
            Assert.Equal(entityRegistrationParams.Properties.Count(), 2);

            //entityRegistrationParams.Properties.Where(p => p.Name)

        }

        public void Dispose()
        {
            // Cleanup
        }
    }
}
