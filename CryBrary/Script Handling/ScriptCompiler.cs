using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Collections.Generic;

namespace CryEngine.Initialization
{
    /// <summary>
    /// Represents a custom script compiler.
    /// </summary>
    public abstract class ScriptCompiler
    {
        #region Statics
        /// <summary>
        /// Validates that a compilation has been successful. 
        /// </summary>
        /// <exception cref="CryEngine.Initialization.ScriptCompilationException">Error found in compilation results</exception> 
        /// <param name="results">The results of the compilation that you wish to validate</param>
        public static void ValidateCompilation(CompilerResults results)
        {
            // Note: Do not use the CompiledAssembly property of the CompilerResults, since that will load the compiled assembly in the current appdomain
            //       Instead, use the PathToAssembly property to check if the assembly has been generated correctly
            if (!results.Errors.HasErrors && results.PathToAssembly != null)
                return;

            string compilationError = string.Format("Compilation failed; {0} errors: ", results.Errors.Count);

            foreach (CompilerError error in results.Errors)
            {
                compilationError += Environment.NewLine;

                if (!error.ErrorText.Contains("(Location of the symbol related to previous error)"))
                    compilationError += string.Format("{0}({1},{2}): {3} {4}: {5}", error.FileName, error.Line, error.Column, error.IsWarning ? "warning" : "error", error.ErrorNumber, error.ErrorText);
                else
                    compilationError += "    " + error.ErrorText;
            }

            throw new ScriptCompilationException(compilationError);
        }
        #endregion

        /// <summary>
        /// Gets all CryScripts located in an assembly
        /// </summary>
        /// <param name="assembly">The assembly to inspect</param>
        /// <returns>A collection of all CryScripts found. In case of ny CryScript instances were found, an empty collection is returned</returns>
        public abstract IEnumerable<CryScript> GetCryScriptsFromAssembly(Assembly assembly);

        /// <summary>
        /// Compiles the scripts found in the scripts folder and returns the path to the compiled assembly
        /// </summary>
        /// <param name="pathToScriptsFolder">Path to the scripts folder</param>
        /// <returns>Path to the compiled assembly</returns>
        public abstract string CompileScriptsIntoAssembly(string pathToScriptsFolder);

    }

    [Serializable]
    public class ScriptCompilationException : Exception
    {
        public ScriptCompilationException() { }

        public ScriptCompilationException(string errorMessage)
            : base(errorMessage) { }

        public ScriptCompilationException(string errorMessage, Exception innerEx)
            : base(errorMessage, innerEx) { }
    }
}
