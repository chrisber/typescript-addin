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
        V8TypescriptProcessor v8TypescriptProcessor;
		
		public TypeScriptContext(IScriptLoader scriptLoader, ILogger logger)
		{
        
		}
		
		public void Dispose()
		{
//			context.Dispose();
		}
		
		public void AddFile(FilePath fileName, string text)
		{
			v8TypescriptProcessor.Host.AddFile(fileName, text);
		}
		
		public void RunInitialisationScript()
		{
			if (runInitialization) {
				runInitialization = false;
                v8TypescriptProcessor.RunMainScript();
			}
		}
		
		public void GetCompletionItemsForTheFirstTime()
		{
			// HACK - run completion on first file so the user does not have to wait about 
			// 1-2 seconds for the completion list to appear the first time it is triggered.
			string fileName = v8TypescriptProcessor.Host.GetFileNames().FirstOrDefault();
			if (fileName != null) {
				GetCompletionItems(fileName, 1, null, false);
			}
		}
		
		public void UpdateFile(FilePath fileName, string text)
		{
			v8TypescriptProcessor.Host.UpdateFile(fileName, text);
		}
		
		public CompletionInfo GetCompletionItems(FilePath fileName, int offset, string text, bool memberCompletion)
		{
            v8TypescriptProcessor.Host.position = offset;
			v8TypescriptProcessor.Host.UpdateFileName(fileName);
			v8TypescriptProcessor.Host.isMemberCompletion = memberCompletion;
			
            v8TypescriptProcessor.RunMemberCompletionScript();
			
			return v8TypescriptProcessor.Host.CompletionResult.result;
		}
		
		public CompletionEntryDetails GetCompletionEntryDetails(FilePath fileName, int offset, string entryName)
		{
			v8TypescriptProcessor.Host.position = offset;
			v8TypescriptProcessor.Host.UpdateFileName(fileName);
			v8TypescriptProcessor.Host.completionEntry = entryName;
			
            v8TypescriptProcessor.RunCompletionDetailsScript();
			
			return v8TypescriptProcessor.Host.CompletionEntryDetailsResult.result;
		}
		
		public SignatureInfo GetSignature(FilePath fileName, int offset)
		{
			v8TypescriptProcessor.Host.position = offset;
			v8TypescriptProcessor.Host.UpdateFileName(fileName);
			
            v8TypescriptProcessor.RunFunctionSignatureScript();
			
			return v8TypescriptProcessor.Host.SignatureResult.result;
		}
		
		public ReferenceEntry[] FindReferences(FilePath fileName, int offset)
		{
			v8TypescriptProcessor.Host.position = offset;
			v8TypescriptProcessor.Host.UpdateFileName(fileName);
			
            v8TypescriptProcessor.RunFindReferencesScript();
			
			return v8TypescriptProcessor.Host.ReferencesResult.result;
		}
		
		public DefinitionInfo[] GetDefinition(FilePath fileName, int offset)
		{
			v8TypescriptProcessor.Host.position = offset;
			v8TypescriptProcessor.Host.UpdateFileName(fileName);
			
            v8TypescriptProcessor.RunDefinitionScript();
			
			return v8TypescriptProcessor.Host.DefinitionResult.result;
		}
		
		public NavigateToItem[] GetLexicalStructure(FilePath fileName)
		{
			v8TypescriptProcessor.Host.UpdateFileName(fileName);
            v8TypescriptProcessor.RunNavigationScript();
			
			return v8TypescriptProcessor.Host.LexicalStructure.result;
		}
		
		public void RemoveFile(FilePath fileName)
		{
			v8TypescriptProcessor.Host.RemoveFile(fileName);
		}
		
		public EmitOutput Compile(FilePath fileName, ITypeScriptOptions options)
		{
			v8TypescriptProcessor.Host.UpdateCompilerSettings(options);
			v8TypescriptProcessor.Host.UpdateFileName(fileName);
            v8TypescriptProcessor.RunLanguageServicesCompileScript();
			
			return v8TypescriptProcessor.Host.CompilerResult.result;
		}
		
		public Diagnostic[] GetSemanticDiagnostics(FilePath fileName, ITypeScriptOptions options)
		{
			v8TypescriptProcessor.Host.UpdateCompilerSettings(options);
			v8TypescriptProcessor.Host.UpdateFileName(fileName);
            v8TypescriptProcessor.RunSemanticDiagnosticsScript();
			
			return v8TypescriptProcessor.Host.SemanticDiagnosticsResult.result;
		}
		
		public void AddFiles(IEnumerable<TypeScriptFile> files)
		{
			foreach (TypeScriptFile file in files) {
				AddFile(file.FileName, file.Text);
			}
		}
	}
}
