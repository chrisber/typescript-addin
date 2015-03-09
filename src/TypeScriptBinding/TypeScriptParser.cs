﻿// 
// TypeScriptParser.cs
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
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Editor;
using ICSharpCode.SharpDevelop.Project;
using ICSharpCode.TypeScriptBinding.Hosting;

namespace ICSharpCode.TypeScriptBinding
{
	public class TypeScriptParser : IParser
	{
		ITypeScriptContextFactory contextFactory;
		
		public TypeScriptParser(ILogger logger)
			: this(new TypeScriptContextFactory(new ScriptLoader(), logger))
		{
		}
		
		public TypeScriptParser(ITypeScriptContextFactory contextFactory)
		{
			this.contextFactory = contextFactory;
		}
		
		public string[] LexerTags { get; set; }
		
		public LanguageProperties Language {
			get { return LanguageProperties.None; }
		}
		
		public IExpressionFinder CreateExpressionFinder(string fileName)
		{
			return null;
		}
		
		public bool CanParse(string fileName)
		{
			return true;
		}
		
		public bool CanParse(IProject project)
		{
			return true;
		}
		
		public ICompilationUnit Parse(IProjectContent projectContent, string fileName, ITextBuffer fileContent)
		{
			return Parse(projectContent, fileName, fileContent, new TypeScriptFile[0]);
		}
		
		public ICompilationUnit Parse(
			IProjectContent projectContent,
			string fileName,
			ITextBuffer fileContent,
			IEnumerable<TypeScriptFile> files)
		{
			try {
				using (TypeScriptContext context = contextFactory.CreateContext()) {
					var file = new FileName(fileName);
					context.AddFile(file, fileContent.Text);
					context.RunInitialisationScript();
					
					NavigationBarItem[] navigation = context.GetNavigationInfo(file);
					var unit = new TypeScriptCompilationUnit(projectContent) {
						FileName = fileName
					};
					unit.AddNavigation(navigation, fileContent);
					
					var typeScriptProjectContent = projectContent as TypeScriptProjectContent;
					if (typeScriptProjectContent != null) {
						context.AddFiles(files);
						IDocument document = DocumentUtilitites.LoadReadOnlyDocumentFromBuffer(fileContent);
						Diagnostic[] diagnostics = context.GetDiagnostics(file, typeScriptProjectContent.Options);
						TypeScriptService.TaskService.Update(diagnostics, file, document);
					}
					
					return unit;
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				LoggingService.Debug(ex.ToString());
			}
			return new DefaultCompilationUnit(projectContent);
		}
		
		public IResolver CreateResolver()
		{
			return null;
		}
		
		public static bool IsTypeScriptFileName(FileName fileName)
		{
			return String.Equals(".ts", fileName.GetExtension(), StringComparison.OrdinalIgnoreCase);
		}
		
		public static bool IsTypeScriptFileName(string fileName)
		{
			return IsTypeScriptFileName(new FileName(fileName));
		}
	}
}
