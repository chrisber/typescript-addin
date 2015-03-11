﻿// 
// TypeScriptCompletionItem.cs
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
using System.Text;
using ICSharpCode.TypeScriptBinding.Hosting;
using MonoDevelop.Core;
using MonoDevelop.Ide.CodeCompletion;
using MonoDevelop.Ide.Gui;

using TypeScriptLanguageService;

namespace ICSharpCode.TypeScriptBinding
{
	public class TypeScriptCompletionData : CompletionData
	{
		CompletionEntry entry;
		CompletionEntryDetailsProvider completionDetailsProvider;
		string description;
		
		public TypeScriptCompletionData(CompletionEntry entry, CompletionEntryDetailsProvider completionDetailsProvider)
			: base(entry.name)
		{
			this.entry = entry;
			this.completionDetailsProvider = completionDetailsProvider;
			Icon = GetIcon(entry);
		}
		
		public override TooltipInformation CreateTooltipInformation(bool smartWrap)
		{
			return new TooltipInformation {
				SignatureMarkup = GetDescription()
			};
		}
		
		string GetDescription()
		{
			if (description == null) {
				CompletionEntryDetails entryDetails = completionDetailsProvider.GetCompletionEntryDetails(entry.name);
				if (entryDetails == null) {
					description = entry.name;
				} else {
					description = GetDescription(entryDetails);
				}
			}
			return description;
		}
		
		string GetDescription(CompletionEntryDetails entryDetails)
		{
			return String.Format(
				"{0} {1}",
				GetFullSymbolName(entryDetails),
				GetDocCommentPrecededByNewLine(entryDetails));
		}
		
		static string GetFullSymbolName(CompletionEntryDetails entryDetails)
		{
			if (entryDetails.displayParts == null) {
				return String.Empty;
			}
			
			var name = new StringBuilder();
			foreach (SymbolDisplayPart part in entryDetails.displayParts) {
				name.Append(part.text);
			}
			return name.ToString();
		}
		
		string GetDocCommentPrecededByNewLine(CompletionEntryDetails entryDetails)
		{
			if ((entryDetails.documentation == null) || (entryDetails.documentation.Length == 0)) {
				return String.Empty;
			}
			
			return String.Format("\r\n{0}", entryDetails.documentation[0].text);
		}
		
		IconId GetIcon(CompletionEntry entry)
		{
			switch (entry.kind) {
				case "property":
					return Stock.Property;
				case "constructor":
				case "getter":
				case "setter":
				case "method":
				case "function":
				case "local function":
					return Stock.Method;
				case "keyword":
					return Stock.Literal;
				case "class":
					return Stock.Class;
				case "var":
				case "local var":
					return Stock.PrivateField;
				case "interface":
					return Stock.Interface;
				case "module":
					return Stock.NameSpace;
				default:
					return null;
			}
		}
	}
}
