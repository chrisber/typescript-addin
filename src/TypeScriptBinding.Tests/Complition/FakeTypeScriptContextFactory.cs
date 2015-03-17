using System;
using ICSharpCode.TypeScriptBinding.Hosting;

namespace TypeScriptBinding.Tests.Complition
{
	public class FakeTypeScriptContextFactory : ITypeScriptContextFactory
	{
		public TypeScriptContext CreateContext()
		{
			var scriptLoader = new ComplitionTestScriptLoader();
			var logger = new LanguageServiceLogger();
			return new TypeScriptContext(scriptLoader, logger);
		}
	}
}

