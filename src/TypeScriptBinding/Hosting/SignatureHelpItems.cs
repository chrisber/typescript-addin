// 
// SignatureInfo.cs
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

namespace ICSharpCode.TypeScriptBinding.Hosting
{
    /// <summary>
    /// TODO Move classes to a seperate file or gather all Typescript.td.ts interfaces 
    /// in one file.
    /// </summary>

    public class SymbolDisplayPart
    {
        public string text { get; set; }

        public string kind { get; set; }

        public override string ToString ()
        {
            return string.Format ("{0}", text);
        }
    }

    public class SignatureHelpParameter
    {
        public  string name { get; set; }

        public  SymbolDisplayPart[] documentation { get; set; }

        public  SymbolDisplayPart[] displayParts { get; set; }

        public  bool isOptional { get; set; }

        public override string ToString ()
        {
            //@TODO Finde out how to change the color
            return String.Format ("{0}", string.Join<SymbolDisplayPart> ("", displayParts));
        }
    }

    public class SignatureHelpItem
    {

        public  bool isVariadic { get; set; }

        public  SymbolDisplayPart[] prefixDisplayParts{ get; set; }

        public  SymbolDisplayPart[]  suffixDisplayParts{ get; set; }

        public  SymbolDisplayPart[]  separatorDisplayParts{ get; set; }

        public   SignatureHelpParameter[] parameters{ get; set; }

        public   SymbolDisplayPart[] documentation{ get; set; }

        public override string ToString ()
        {

            return string.Join<SymbolDisplayPart> ("", prefixDisplayParts) +
            string.Join<SignatureHelpParameter> ("", parameters) +
            string.Join<SymbolDisplayPart> ("", suffixDisplayParts) +
                (string.Join<SymbolDisplayPart> ("", separatorDisplayParts)).Replace(",","");
        }
    }

    public class SignatureHelpItems
    {
        public SignatureHelpItems ()
        {
            this.items = new SignatureHelpItem[0];
        }

        public  SignatureHelpItem[] items { get; set; }

        public  TextSpan applicableSpan{ get; set; }

        public  int selectedItemIndex{ get; set; }

        public  int argumentIndex{ get; set; }

        public  int argumentCount{ get; set; }

    }
}
