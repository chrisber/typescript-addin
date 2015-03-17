using System;
using System.IO;
using ICSharpCode.TypeScriptBinding.Hosting;
using MonoDevelop.Projects;
using System.Reflection;

namespace TypeScriptBinding.Tests.Complition
{
	public class ComplitionTestScriptLoader : IScriptLoader
	{
		string execDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        char dS = Path.DirectorySeparatorChar;
      

		public ComplitionTestScriptLoader()
		{
		}

		string GetScriptFromResource(string scriptName)
		{
			Stream stream = this.GetType().Assembly.GetManifestResourceStream(scriptName);
			return new StreamReader(stream).ReadToEnd();
		}

		#region IScriptLoader implementation
		public string GetComplitionTestFullNameScript()
		{
				string relativePath = ".." + dS + ".." + dS + "Complition" + dS + "complition.ts";
				string fullname = Path.GetFullPath(
				Path.Combine(
					execDir, 
				    relativePath
					)
				);
				return fullname;
		}

		public string GetTypeScriptServicesScript()
		{
			return GetScriptFromResource("typescriptServices.js");
		}

		public string GetLibScript()
		{
			return GetScriptFromResource("lib.d.ts");
		}

		public string RootFolder {
			get {
				throw new NotImplementedException();
			}
		}

		public string TypeScriptCompilerFileName {
			get {
				throw new NotImplementedException();
			}
		}

		public string LibScriptFileName {
			get {
				string relativePath =  ".." + dS + ".." + dS  + ".." + dS + "TypeScriptBinding" + dS  + "Scripts" + dS  + "lib.d.ts";
				string fullname = Path.GetFullPath(
				Path.Combine(
					execDir, 
				    relativePath
					)
				);
				return fullname;
			}
		}

		#endregion
	}
}

