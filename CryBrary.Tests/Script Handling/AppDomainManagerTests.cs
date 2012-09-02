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
        private int scriptDomainIdSet = Int32.MinValue;
        private int ScriptDomainIdSetByAppDomainId = Int32.MinValue;

        protected override void ConfigureMocks()
        {
            base.ConfigureMocks();

            var appDomainMock = GetMock<INativeAppDomainMethods>();

            appDomainMock.Setup(x => x.SetScriptAppDomain(It.IsAny<int>())).Callback<int>(x =>
                                                                                              {
                                                                                                  _executor.Execute(SetValues, x);
                                                                                                  scriptDomainIdSet = x;
                                                                                                  ScriptDomainIdSetByAppDomainId
                                                                                                      =
                                                                                                      AppDomain.
                                                                                                          CurrentDomain.
                                                                                                          Id;
                                                                                              });
        }

        private void SetValues(int x)
        {
            scriptDomainIdSet = x;
        }

        [Test]
        public void InitializeScriptDomain_InitialDomain_CreatedSuccesfully()
        {
            // Arrange
            var appDomainManager = new CryEngine.AppDomainManager();
            _executor = new AppDomainExecutor();

            // We need to reinitialize the mocks for each appdomain
            appDomainManager.ScriptDomainCreated += (s, e) => appDomainManager.Loader.Execute(InitializeMocks);

            // Act
            appDomainManager.InitializeScriptDomain(AppDomain.CurrentDomain.BaseDirectory);

            // Assert
            Assert.IsNotNull(appDomainManager.ScriptAppDomain);
            Assert.AreNotEqual(AppDomain.CurrentDomain.Id, appDomainManager.ScriptAppDomain.Id);
            //Assert.AreEqual(appDomainManager.ScriptAppDomain.Id, scriptDomainIdSet);
        }

    }
}
