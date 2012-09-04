using System;
using CryEngine.Native;
using Moq;
using NUnit.Framework;

namespace CryBrary.Tests.ScriptHandling
{
    [TestFixture]
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

        [Test]
        public void InitializeScriptDomain_InitialDomain_CreatedSuccesfully()
        {
            // Arrange
            var appDomainManager = CreateAppDomainManager();

            // Act
            appDomainManager.InitializeScriptDomain(AppDomain.CurrentDomain.BaseDirectory);

            // Assert
            Assert.IsNotNull(appDomainManager.ScriptAppDomain);
            Assert.AreNotEqual(AppDomain.CurrentDomain.Id, appDomainManager.ScriptAppDomain.Id);
            Assert.AreEqual(appDomainManager.ScriptAppDomain.Id, _storage.Values["ScriptDomainId"]);
        }
    }
}
