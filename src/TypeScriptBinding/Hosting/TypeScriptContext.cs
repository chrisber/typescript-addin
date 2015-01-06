// 
// TypeScriptContext.cs
// 
// Author:
//   Matt Ward <ward.matt@gmail.com>
// 
// Copyright (C) 2013 Matthew Ward
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MonoDevelop.Core;
//using Noesis.Javascript;
using TypeScriptHosting;
using V8.Net;

namespace ICSharpCode.TypeScriptBinding.Hosting
{
	public class TypeScriptContext : IDisposable
	{
//		JavascriptContext context = new JavascriptContext();
		IScriptLoader scriptLoader;
		bool runInitialization = true;
         
		
		public TypeScriptContext(IScriptLoader scriptLoader, ILogger logger)
		{
        
		}
		
		public void Dispose()
		{
//			context.Dispose();
		}
		
		public bool AddFile(FilePath fileName, string text)
		{
			return V8TypescriptProcessor.host().AddFile(fileName, text);
		}
		
		public void RunInitialisationScript()
		{
			if (runInitialization) {
				runInitialization = false;
                V8TypescriptProcessor.RunMainScript();
			}
		}
		
		public void GetCompletionItemsForTheFirstTime()
		{
			// HACK - run completion on first file so the user does not have to wait about 
			// 1-2 seconds for the completion list to appear the first time it is triggered.
			string fileName = V8TypescriptProcessor.host().GetFileNames().FirstOrDefault();
			if (fileName != null) {
				GetCompletionItems(fileName, 1, null, false);
			}
		}
		
		public void UpdateFile(FilePath fileName, string text)
		{
			V8TypescriptProcessor.host().UpdateFile(fileName, text);
		}
		
		public CompletionInfo GetCompletionItems(FilePath fileName, int offset, string text, bool memberCompletion)
		{
            V8TypescriptProcessor.host().position = offset;
			V8TypescriptProcessor.host().UpdateFileName(fileName);
			V8TypescriptProcessor.host().isMemberCompletion = memberCompletion;
			
            V8TypescriptProcessor.RunMemberCompletionScript();
			
			return V8TypescriptProcessor.host().CompletionResult.result;
		}
		
		public CompletionEntryDetails GetCompletionEntryDetails(FilePath fileName, int offset, string entryName)
		{
			V8TypescriptProcessor.host().position = offset;
			V8TypescriptProcessor.host().UpdateFileName(fileName);
			V8TypescriptProcessor.host().completionEntry = entryName;
			
            V8TypescriptProcessor.RunCompletionDetailsScript();
			
			return V8TypescriptProcessor.host().CompletionEntryDetailsResult.result;
		}
		
		public SignatureHelpItems GetSignature(FilePath fileName, int offset)
		{
			V8TypescriptProcessor.host().position = offset;
			V8TypescriptProcessor.host().UpdateFileName(fileName);
			
            V8TypescriptProcessor.RunFunctionSignatureScript();
			
			return V8TypescriptProcessor.host().SignatureResult.result;
		}
		
		public ReferenceEntry[] FindReferences(FilePath fileName, int offset)
		{
			V8TypescriptProcessor.host().position = offset;
			V8TypescriptProcessor.host().UpdateFileName(fileName);
			
            V8TypescriptProcessor.RunFindReferencesScript();
			
			return V8TypescriptProcessor.host().ReferencesResult.result;
		}
		
		public DefinitionInfo[] GetDefinition(FilePath fileName, int offset)
		{
			V8TypescriptProcessor.host().position = offset;
			V8TypescriptProcessor.host().UpdateFileName(fileName);
			
            V8TypescriptProcessor.RunDefinitionScript();
			
			return V8TypescriptProcessor.host().DefinitionResult.result;
		}
		
        public NavigateToItem[] GetLexicalStructure(FilePath fileName)
		{
			V8TypescriptProcessor.host().UpdateFileName(fileName);
            V8TypescriptProcessor.RunNavigationScript();
			
            return V8TypescriptProcessor.host().LexicalStructure.result;
		}
		
		public void RemoveFile(FilePath fileName)
		{
			V8TypescriptProcessor.host().RemoveFile(fileName);
		}
		
		public EmitOutput Compile(FilePath fileName, ITypeScriptOptions options)
		{
			V8TypescriptProcessor.host().UpdateCompilerSettings(options);
			V8TypescriptProcessor.host().UpdateFileName(fileName);
            V8TypescriptProcessor.RunLanguageServicesCompileScript();
			
			return V8TypescriptProcessor.host().CompilerResult.result;
		}
		
		public Diagnostic[] GetSemanticDiagnostics(FilePath fileName, ITypeScriptOptions options)
		{
			V8TypescriptProcessor.host().UpdateCompilerSettings(options);
			V8TypescriptProcessor.host().UpdateFileName(fileName);
            V8TypescriptProcessor.RunSemanticDiagnosticsScript();
			
			return V8TypescriptProcessor.host().SemanticDiagnosticsResult.result;
		}
		
		public void AddFiles(IEnumerable<TypeScriptFile> files)
		{
			foreach (TypeScriptFile file in files) {
				AddFile(file.FileName, file.Text);
			}
		}
	}
}
