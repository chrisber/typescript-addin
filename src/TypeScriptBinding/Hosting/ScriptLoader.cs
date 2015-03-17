﻿// 
// ScriptLoader.cs
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
using System.IO;

namespace ICSharpCode.TypeScriptBinding.Hosting
{
	public class ScriptLoader : IScriptLoader
	{
		string root;
		string typeScriptServicesFileName;
		string libScriptFileName;

		
		public ScriptLoader()
		{
			string addinPath = Path.GetDirectoryName(typeof(ScriptLoader).Assembly.Location);
			root = Path.Combine(addinPath, "Scripts");
			root = Path.GetFullPath(root);
			
			typeScriptServicesFileName = GetFullPath("typescriptServices.js");
			libScriptFileName = GetFullPath("lib.d.ts");

		}
		
		public string RootFolder {
			get { return root; }
		}
		
		public string LibScriptFileName {
			get { return libScriptFileName; }
		}
		
		string GetFullPath(string fileName)
		{
			return Path.Combine(root, fileName);
		}

        string ReadScript(string fileName)
        {
            return File.ReadAllText(fileName);
        }

		public string GetTypeScriptServicesScript()
		{
			return ReadScript(typeScriptServicesFileName);
		}
		
		public string GetLibScript()
		{
			return ReadScript(libScriptFileName);
		}

	}
}
