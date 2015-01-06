﻿// 
// TypeScriptParsedDocument.cs
// 
// Author:
//   Matt Ward <ward.matt@gmail.com>
// 
// Copyright (C) 2014 Matthew Ward
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
using System.Linq;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.TypeScriptBinding.Hosting;
using MonoDevelop.Ide.TypeSystem;

namespace ICSharpCode.TypeScriptBinding
{
	public class TypeScriptParsedDocument : ParsedDocument
	{
		List<Error> errors = new List<Error>();
		
		public TypeScriptParsedDocument(string fileName)
			: base(fileName)
		{
			Flags = ParsedDocumentFlags.NonSerializable;
		}
		
		public void AddNavigation(NavigateToItem[] navigation, IDocument document)
		{
			foreach (NavigateToItem item in navigation) {
				switch (item.kind) {
					case "class":
					case "interface":
					case "module":
						AddClass(item, document);
						break;
					case "method":
					case "constructor":
						AddMethod(item, document);
						break;
				}
			}
		}
		
		void AddClass(NavigateToItem item, IDocument document)
		{
			DomRegion region = item.ToRegionStartingFromOpeningCurlyBrace(document);
			var folding = new FoldingRegion(region, FoldType.Type);
			Add(folding);
		}
		
		void AddMethod(NavigateToItem item, IDocument document)
		{
			DomRegion region = item.ToRegionStartingFromOpeningCurlyBrace(document);
			var folding = new FoldingRegion(region, FoldType.Member);
			Add(folding);
		}
		
		public void AddDiagnostics(Diagnostic[] diagnostics, IDocument document)
		{
			errors = diagnostics
				.Select(diagnostic => CreateError(diagnostic, document))
				.ToList();
		}
		
		Error CreateError(Diagnostic diagnostic, IDocument document)
		{
			TextLocation location = document.GetLocation(diagnostic.start);

   if(diagnostic.category.Equals(DiagnosticCategory.Warning)){
     return new Error( ErrorType.Warning,diagnostic.ToString(),location);
   }
   if(diagnostic.category.Equals(DiagnosticCategory.Message)){
     return new Error( ErrorType.Unknown,diagnostic.ToString(),location);
   }

   return new Error( ErrorType.Error,diagnostic.ToString(),location);
		}
		
		public override IList<Error> Errors {
			get { return errors; }
		}
	}
}
