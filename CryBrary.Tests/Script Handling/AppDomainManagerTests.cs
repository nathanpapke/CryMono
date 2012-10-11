using System;
using CryEngine.Native;
using Moq;
using Xunit;

namespace CryBrary.Tests.ScriptHandling
{
    [Serializable]
    public class AppDomainManagerTests : CryBraryTests
    {
        private AppDomainExecutor _executor;
        private CrossAppDomainStorage _storage;

        protected override void ConfigureMocks()
        {
            base.ConfigureMocks();

            var appDomainMock = GetMock<INativeAppDomainMethods>();

            appDomainMock.Setup(x => x.SetScriptAppDomain(It.IsAny<int>())).Callback<int>(x => _executor.Execute(SetValues, x));
        }

        private void SetValues(int x)
        {
            _storage.Values.Add("ScriptDomainId", x);
        }

        private CryEngine.AppDomainManager CreateAppDomainManager()
        {
            var appDomainManager = new CryEngine.AppDomainManager();
            _executor = new AppDomainExecutor();
            _storage = new CrossAppDomainStorage();

            // We need to reinitialize the mocks for each appdomain
            appDomainManager.ScriptDomainCreated += (s, e) => appDomainManager.Loader.Execute(InitializeMocks);

            return appDomainManager;
        }

        [Fact]
        public void InitializeScriptDomain_InitialDomain_CreatedSuccesfully()
        {
            // Arrange
            var appDomainManager = CreateAppDomainManager();

            // Act
            appDomainManager.InitializeScriptDomain(AppDomain.CurrentDomain.BaseDirectory);

            // Assert
            Assert.NotNull(appDomainManager.ScriptAppDomain);
            Assert.NotEqual(AppDomain.CurrentDomain.Id, appDomainManager.ScriptAppDomain.Id);
            Assert.Equal(appDomainManager.ScriptAppDomain.Id, _storage.Values["ScriptDomainId"]);
        }
    }
}