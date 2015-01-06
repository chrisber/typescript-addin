﻿// 
// TypeScriptParameterDataProvider.cs
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
using ICSharpCode.TypeScriptBinding.Hosting;
using Mono.TextEditor;
using MonoDevelop.Ide.CodeCompletion;

namespace ICSharpCode.TypeScriptBinding
{
	public class TypeScriptParameterDataProvider : ParameterDataProvider
	{
		TypeScriptContext context;
		SignatureHelpItems signatureInfo;
		
		public TypeScriptParameterDataProvider(TypeScriptContext context, int startOffset)
			: base(startOffset)
		{
			this.context = context;
		}
		
		public void GetSignatures(TextEditorData editor)
		{
			signatureInfo = context.GetSignature(editor.FileName, editor.Caret.Offset);
			if (signatureInfo == null) {
				signatureInfo = new SignatureHelpItems ();
			}
		}
		
		public override string GetParameterName(int overload, int paramIndex)
		{
            return GetSignatureOverload(overload).parameters[paramIndex].name;
		}
		
		public override int GetParameterCount(int overload)
		{

            return GetSignatureOverload (overload).ToString().Length;

		}
		
        SignatureHelpItem GetSignatureOverload(int overload)
		{
			return signatureInfo.items[overload];
		}
		
		public override int Count {
			get { return signatureInfo.items.Length; }
		}
		
		public override bool AllowParameterList(int overload)
		{
			return GetSignatureOverload(overload).parameters.Length > 0;
		}
		
		public override TooltipInformation CreateTooltipInformation(int overload, int currentParameter, bool smartWrap)
		{
            SignatureHelpItem signatureOverload = GetSignatureOverload(overload);
			
			return new TooltipInformation {
                SignatureMarkup = signatureOverload.ToString(),
                SummaryMarkup = string.Join<SymbolDisplayPart>("",signatureOverload.documentation)
			};
		}
	}
}
