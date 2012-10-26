using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CryEngine.Extensions;
using CryEngine.Initialization;
using CryEngine.Testing;
using CryEngine.Utilities;
using CryEngine.Compilers.NET.Handlers;

namespace CryEngine.Compilers.NET
{
	public class NetCompiler : ScriptCompiler
	{
	    private readonly ScriptRegistrationParamsFactory _scriptRegistrationFactory;

	    public NetCompiler()
	    {
	        _scriptRegistrationFactory = new ScriptRegistrationParamsFactory();

            // Register registration handlers here
            _scriptRegistrationFactory.Register<ActorScriptRegistrationHandler>(ScriptType.Actor);
            _scriptRegistrationFactory.Register<CryScriptInstanceScriptRegistrationHandler>(ScriptType.CryScriptInstance);
            _scriptRegistrationFactory.Register<EntityScriptRegistrationHandler>(ScriptType.Entity);
            _scriptRegistrationFactory.Register<FlowNodeScriptRegistrationHandler>(ScriptType.FlowNode);
            _scriptRegistrationFactory.Register<GameRulesScriptRegistrationHandler>(ScriptType.GameRules);
	    }

        public override IEnumerable<CryScript> GetCryScriptsFromAssembly(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            var foundScripts = new List<CryScript>();

            var typesInAssembly = assembly.GetTypes().Where(t => !t.ContainsAttribute<ExcludeFromCompilationAttribute>());
            foreach (var type in typesInAssembly)
            {
                if (!type.ContainsAttribute<ExcludeFromCompilationAttribute>())
                {
                    var scriptsFoundInType = GetCryScriptsFromType(type);
                    foundScripts.AddRange(scriptsFoundInType);
                }

                // Find tests, this seems out of place
                if (type.ContainsAttribute<TestCollectionAttribute>())
                {
                    RegisterTestCollection(type);
                }
            }

            return foundScripts;
        }

        private void RegisterTestCollection(Type type)
        {
            if (type.ContainsAttribute<TestCollectionAttribute>())
            {
                var ctor = type.GetConstructor(Type.EmptyTypes);
                if (ctor != null)
                {
                    var collection = new TestCollection
                    {
                        Instance = ctor.Invoke(Type.EmptyTypes),
                        Tests = from method in type.GetMethods()
                                where method.ContainsAttribute<TestAttribute>()
                                    && method.GetParameters().Length == 0
                                select method
                    };

                    TestManager.TestCollections.Add(collection);
                }
            }
        }

        protected virtual IEnumerable<CryScript> GetCryScriptsFromType(Type type)
        {
            CryScript cryScript;
            bool scriptCreatedSuccessfully = CryScript.TryCreate(type, out cryScript);
            
            if (scriptCreatedSuccessfully)
            {
                var scriptRegistrationParams = GetScriptRegistrationParamsFromCryScript(cryScript);
                cryScript.RegistrationParams.AddRange(scriptRegistrationParams);
                yield return cryScript;
            }
        }

        protected virtual IEnumerable<IScriptRegistrationParams> GetScriptRegistrationParamsFromCryScript(CryScript cryScript)
        {
            return _scriptRegistrationFactory.GetScriptRegistrationParams(cryScript).Where(x => x != null);
        }

        public override string CompileScriptsIntoAssembly(string pathToScriptsFolder)
        {
            throw new NotImplementedException();
        }


		public override IEnumerable<CryScript> Process(IEnumerable<Assembly> assemblies)
		{
            /*var scripts = new List<CryScript>();

            foreach (var assembly in assemblies)
                scripts.AddRange(ProcessAssembly(assembly));

            scripts.AddRange(ProcessAssembly(CompileCSharpFromSource()));
          //  scripts.AddRange(ProcessAssembly(CompileVisualBasicFromSource()));

            return scripts;*/

            throw new NotSupportedException();
		}

        Assembly CompileVisualBasicFromSource()
        {
            return CompileFromSource(CodeDomProvider.CreateProvider("VisualBasic"), "*.vb");
        }

		Assembly CompileCSharpFromSource()
        {
            return CompileFromSource(CodeDomProvider.CreateProvider("CSharp"), "*.cs");
        }

        Assembly CompileFromSource(CodeDomProvider provider, string searchPattern)
		{
			var compilerParameters = new CompilerParameters();

			compilerParameters.GenerateExecutable = false;

			// Necessary for stack trace line numbers etc
			compilerParameters.IncludeDebugInformation = true;
			compilerParameters.GenerateInMemory = false;

#if RELEASE
			if(!compilationParameters.ForceDebugInformation)
			{
				compilerParameters.GenerateInMemory = true;
				compilerParameters.IncludeDebugInformation = false;
			}
#endif

			if(!compilerParameters.GenerateInMemory)
			{
                var assemblyPath = Path.Combine(PathUtils.TempFolder, string.Format("CompiledScripts_{0}.dll", searchPattern.Replace("*.", "")));

				if(File.Exists(assemblyPath))
				{
					try
					{
						File.Delete(assemblyPath);
					}
					catch(Exception ex)
					{
						if(ex is UnauthorizedAccessException || ex is IOException)
							assemblyPath = Path.ChangeExtension(assemblyPath, "_" + Path.GetExtension(assemblyPath));
						else
							throw;
					}
				}

				compilerParameters.OutputAssembly = assemblyPath;
			}

			var scripts = new List<string>();
			var scriptsDirectory = PathUtils.ScriptsFolder;

			if(Directory.Exists(scriptsDirectory))
			{
                foreach (var script in Directory.GetFiles(scriptsDirectory, searchPattern, SearchOption.AllDirectories))
					scripts.Add(script);
			}
			else
				Debug.LogAlways("Scripts directory could not be located");

			CompilerResults results;
            using(provider)
			{
				var referenceHandler = new AssemblyReferenceHandler();
				compilerParameters.ReferencedAssemblies.AddRange(referenceHandler.GetRequiredAssembliesFromFiles(scripts));

				results = provider.CompileAssemblyFromFile(compilerParameters, scripts.ToArray());
			}

			return ScriptCompiler.ValidateCompilation(results);
		}
	}
}
