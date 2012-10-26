using System;
using System.Linq;
using CryEngine;
using CryEngine.Initialization;
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

            var entityMock = GetMock<INativeEntityMethods>();
            entityMock.Setup(x => x.SetWorldPos(It.IsAny<IntPtr>(), It.IsAny<Vec3>())).Callback<IntPtr, Vec3>((i, v) => _executor.Execute(SetValues, "positionSet", (object)v));
            entityMock.Setup(x => x.SetFlags(It.IsAny<IntPtr>(), It.IsAny<EntityFlags>())).Callback<IntPtr, EntityFlags>((i, f) => _executor.Execute(SetValues, "flagsSet", (object)f));
        }

        private void SetValues(int x)
        {
            _storage.Values["ScriptDomainId"] = x;
        }
        private void SetValues(string key, object value)
        {
            _storage.Values[key] = value;
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

        [Fact]
        public void ReloadScriptDomain_AfterInitialization_NewAppDomainLoaded()
        {
            // Arrange
            var appDomainManager = CreateAppDomainManager();

            // Act
            appDomainManager.InitializeScriptDomain(AppDomain.CurrentDomain.BaseDirectory);
            int originalAppDomainId = appDomainManager.ScriptAppDomain.Id;
            bool reloadResult = appDomainManager.Reload();

            // Assert
            Assert.True(reloadResult);
            Assert.NotEqual(originalAppDomainId, appDomainManager.ScriptAppDomain.Id);
        }

        [Entity]
        public class MyTestEntity : EntityBase
        {
            [EditorProperty]
            public bool Active { get; set; }

            public int Health { get; set; }

        }

        [Fact]
        public void ReloadScriptDomain_WithSimpleEntity_DataRetained()
        {
            // Arrange
            var appDomainManager = CreateAppDomainManager();

            // Act
            appDomainManager.InitializeScriptDomain(AppDomain.CurrentDomain.BaseDirectory);
            appDomainManager.Loader.Execute(() =>
                                  {
                                      var entity = new MyTestEntity() { Active = true, Health = 200, Flags = EntityFlags.CastShadow | EntityFlags.ClientOnly, Position = new Vec3(2, 66, 2) };
                                      ScriptManager.Instance.AddScriptInstance(entity, ScriptType.Entity);
                                  });
            int originalAppDomainId = appDomainManager.ScriptAppDomain.Id;
            bool reloadResult = appDomainManager.Reload();

            // Assert
            Assert.True(reloadResult);
            Assert.NotEqual(originalAppDomainId, appDomainManager.ScriptAppDomain.Id);

            // Asserts in script domain
            appDomainManager.Loader.Execute(() =>
                                                {
                                                    Assert.NotEmpty(ScriptManager.Instance.Scripts);

                                                    var entity = ScriptManager.Instance.Scripts.Single(c => c.Type == typeof(MyTestEntity)).ScriptInstances.First() as MyTestEntity;

                                                    Assert.Equal(entity.Active, true);
                                                    Assert.Equal(entity.Health, 200);
                                                });


            Assert.Equal(_storage.Values["positionSet"], new Vec3(2, 66, 2));

            var entityFlags = (EntityFlags)_storage.Values["flagsSet"];

            Assert.True(entityFlags.HasFlag(EntityFlags.CastShadow));
            Assert.True(entityFlags.HasFlag(EntityFlags.ClientOnly));
        }
    }
}