﻿
using System;
using ICSharpCode.TypeScriptBinding;
using ICSharpCode.TypeScriptBinding.Hosting;

namespace TypeScriptBinding.Tests.Hosting
{
	public class TestTypeScriptOptions : ITypeScriptOptions
	{
		public bool RemoveComments { get; set; }
		public bool GenerateSourceMap { get; set; }
		public string ModuleKind { get; set; }
		public string EcmaScriptVersion { get; set; }
		
		public LanguageVersion GetLanguageVersion()
		{
			throw new NotImplementedException();
		}
		
		public ModuleGenTarget GetModulearget()
		{
			throw new NotImplementedException();
		}
	}
}
