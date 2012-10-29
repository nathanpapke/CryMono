using CryEngine;
using CryEngine.Compilers.Net;
using CryEngine.Initialization;
using System;
using System.Linq;
using Xunit;
using System.Reflection;
using NETFramework.Tests.Samples;
using System.IO;
using CryBrary.Tests;

namespace NETFramework.Tests
{
    public class CompilerTests : CryBraryTests,IDisposable
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

            Assert.Equal(entityRegistrationParams.Properties.Single(p => p.Name == "Enabled").Type, EntityPropertyType.Bool);
        }

        [Fact]
        public void GetCryScriptsFromAssembly_TestAssembly_ContainsCorrectActorScript()
        {
            // Arrange
            var compiler = new NetCompiler();

            // Act
            var foundScripts = compiler.GetCryScriptsFromAssembly(Assembly.GetExecutingAssembly());

            // Assert
            var actorScript = foundScripts.Single(c => c.Type == typeof(SampleActor));
            Assert.NotEmpty(actorScript.RegistrationParams);
            Assert.Equal(actorScript.RegistrationParams.Count, 2); // It is an actor and an entity
            Assert.Equal(actorScript.ScriptType & ScriptType.Actor, ScriptType.Actor);
            Assert.Single(actorScript.RegistrationParams.OfType<ActorRegistrationParams>());
        }

        [Fact]
        public void GetCryScriptsFromAssembly_TestAssembly_ContainsCorrectGameRulesScript()
        {
            // Arrange
            var compiler = new NetCompiler();

            // Act
            var foundScripts = compiler.GetCryScriptsFromAssembly(Assembly.GetExecutingAssembly());

            // Assert
            var gameRulesScript = foundScripts.Single(c => c.Type == typeof(SampleGameRules));
            Assert.NotEmpty(gameRulesScript.RegistrationParams);
            Assert.Equal(gameRulesScript.RegistrationParams.Count, 2); // Gamerules and entity
            Assert.Equal(gameRulesScript.ScriptType & ScriptType.GameRules, ScriptType.GameRules);
            Assert.Single(gameRulesScript.RegistrationParams.OfType<GameRulesRegistrationParams>());

            var gameRulesRegistrationParams = gameRulesScript.RegistrationParams.OfType<GameRulesRegistrationParams>().Single();
            Assert.True(gameRulesRegistrationParams.DefaultGamemode);
            Assert.Equal(gameRulesRegistrationParams.Name, "MyGameRules");

        }

        [Fact]
        public void GetCryScriptsFromAssembly_TestAssembly_ContainsCorrectFlowNodeScript()
        {
            // Arrange
            var compiler = new NetCompiler();

            // Act
            var foundScripts = compiler.GetCryScriptsFromAssembly(Assembly.GetExecutingAssembly());

            // Assert
            var flowNodeScript = foundScripts.Single(c => c.Type == typeof(SampleFlowNode));
            Assert.NotEmpty(flowNodeScript.RegistrationParams);
            Assert.Equal(flowNodeScript.RegistrationParams.Count, 1);
            Assert.Equal(flowNodeScript.ScriptType & ScriptType.FlowNode, ScriptType.FlowNode);
            Assert.Single(flowNodeScript.RegistrationParams.OfType<FlowNodeRegistrationParams>());
        }

        [Fact]
        public void CompileScriptsIntoAssembly_NullPath_ArgumentNullException()
        {
            // Arrange
            var compiler = new NetCompiler();

            // Act
            Assert.ThrowsDelegate action = () => compiler.CompileScriptsIntoAssembly(null);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void CompileScriptsIntoAssembly_NonExistingPath_DirectoryNotFoundException()
        {
            // Arrange
            var compiler = new NetCompiler();

            // Act
            Assert.ThrowsDelegate action = () => compiler.CompileScriptsIntoAssembly(@"yz:test");

            // Assert
            Assert.Throws<DirectoryNotFoundException>(action);
        }

        [Fact]
        public void CompileScriptsIntoAssembly_PathNotContainingAnyScripts_NoAssemblyBuilt()
        {
            string sourceDirectory = Path.Combine(Path.GetTempPath(), "CryMonoTests");
            if (!Directory.Exists(sourceDirectory))
                Directory.CreateDirectory(sourceDirectory);
            try
            {
                // Arrange
                var compiler = new NetCompiler();

                // Act
                string assemblyBuilt = compiler.CompileScriptsIntoAssembly(sourceDirectory);

                // Assert
                Assert.Null(assemblyBuilt);
            } finally
            {
                if (Directory.Exists(sourceDirectory))
                    Directory.Delete(sourceDirectory);
            }
        }

        [Fact]
        public void CompileScriptsIntoAssembly_PathContaingCSharpFiles_AssemblyBuilt()
        {
            string sourceDirectory = Path.Combine(Path.GetTempPath(), "CryMonoTests");
            if (!Directory.Exists(sourceDirectory))
                Directory.CreateDirectory(sourceDirectory);
            try
            {
                // Arrange
                var compiler = new NetCompiler();
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NETFramework.Tests.Samples.Raw_SampleEntity.cs"))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string outputfileName = Path.Combine(sourceDirectory, "sourcefile1.cs");
                        File.WriteAllText(outputfileName, reader.ReadToEnd());
                    }
                }


                // Act
                string assemblyBuilt = compiler.CompileScriptsIntoAssembly(sourceDirectory);

                // Assert
                Assert.NotNull(assemblyBuilt);
                Assert.True(File.Exists(assemblyBuilt));
            }
            finally
            {
                if (Directory.Exists(sourceDirectory))
                    Directory.Delete(sourceDirectory, true);
            }
        }


        [Fact]
        public void CompileScriptsIntoAssembly_PathContaingVbFiles_AssemblyBuilt()
        {
            string sourceDirectory = Path.Combine(Path.GetTempPath(), "CryMonoTests");
            if (!Directory.Exists(sourceDirectory))
                Directory.CreateDirectory(sourceDirectory);
            try
            {
                // Arrange
                var compiler = new NetCompiler();
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NETFramework.Tests.Samples.Raw_SampleEntity.vb"))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string outputfileName = Path.Combine(sourceDirectory, "sourcefile1.vb");
                        File.WriteAllText(outputfileName, reader.ReadToEnd());
                    }
                }


                // Act
                string assemblyBuilt = compiler.CompileScriptsIntoAssembly(sourceDirectory);

                // Assert
                Assert.NotNull(assemblyBuilt);
                Assert.True(File.Exists(assemblyBuilt));
            }
            finally
            {
                if (Directory.Exists(sourceDirectory))
                    Directory.Delete(sourceDirectory, true);
            }
        }

        public void Dispose()
        {
            // Cleanup
        }
    }
}
