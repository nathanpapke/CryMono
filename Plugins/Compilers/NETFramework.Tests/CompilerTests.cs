using CryEngine;
using CryEngine.Compilers.NET;
using CryEngine.Initialization;
using System;
using System.Linq;
using Xunit;
using System.Reflection;
using NETFramework.Tests.Samples;

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


        public void Dispose()
        {
            // Cleanup
        }
    }
}
