// 
// CompilerSettings.cs
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
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using V8.Net;

namespace ICSharpCode.TypeScriptBinding.Hosting
{
    [ScriptObject("CompilerOptions", security: ScriptMemberSecurity.Permanent)]
	public class CompilerOptions
	{
		public CompilerOptions(ITypeScriptOptions options)
			: this()
		{
            diagnostics = true;
            removeComments = options.RemoveComments;
            sourceMap = options.GenerateSourceMap;
            noImplicitAny = options.NoImplicitAny;
            target = options.GetLanguageVersion();
            outFile = options.GetOutputFileFullPath();
            outDir = options.GetOutputDirectoryFullPath();

		}
		
		public CompilerOptions()
		{
            outFile = "";
			outDir = "";
			mapRoot = "";
			sourceRoot = "";
            target = ScriptTarget.ES5;
            module = ModuleKind.None;
			noImplicitAny = true;
            declaration = true;
            diagnostics = true;
		}
		
        public string charset { get; set; }
        public int codepage { get; set; }
        public bool declaration { get; set; }
        public bool diagnostics { get; set; }
        public bool emitBOM { get; set; }
        public bool help { get; set; }
        public string locale { get; set; }
        public string mapRoot { get; set; }
        public ModuleKind module { get; set; }
        public bool noErrorTruncation { get; set; }
        public bool noImplicitAny { get; set; }
        public bool noLib { get; set; }
        public bool noLibCheck { get; set; }
        public bool noResolve { get; set; }
        [ScriptMember("out", security: ScriptMemberSecurity.Permanent)]
        public string outFile { get; set; }
        public string outDir { get; set; }
        public bool removeComments { get; set; }
        public bool sourceMap { get; set; }
        public string sourceRoot { get; set; }
        public ScriptTarget target { get; set; }
        public bool version { get; set; }
        public bool watch { get; set; }
        public List<KeyValuePair<string, object>> option { get; set; }


	}
}
