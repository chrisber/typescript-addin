using System;

using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory.TypeSystem;

using TypeScriptLanguageService;

namespace ICSharpCode.TypeScriptBinding
{
    public class NavigationBarItemRegion
    {
        NavigationBarItem navItem = null;

        public NavigationBarItemRegion(NavigationBarItem navItem)
        {
            this.navItem = navItem;
        }

        internal int minChar {
            get {
                if (HasSpans) {
                    // TODO not sure if this is becouse of line endings crlf to lf
                    // needs to be tested on windows
                    return navItem.spans[0].start + 1;
                }
                return 0;
            }
        }

        internal bool HasSpans {
            get { return (navItem.spans != null) && (navItem.spans.Length > 0); }
        }

        internal int limChar {
            get {
                if (HasSpans) {
                    TextSpan span = navItem.spans[0];
                    return span.start + span.length;
                }
                return 0;
            }
        }

        internal DomRegion ToRegionStartingFromOpeningCurlyBrace(IDocument document)
        {
            int startOffset = GetOpeningCurlyBraceOffsetForRegion(document);
            TextLocation start = document.GetLocation(startOffset);
            // TODO not sure if this is becouse of line endings crlf to lf
            // needs to be tested on windows
            TextLocation end = document.GetLocation(limChar);
            return new DomRegion(start, end);
        }

        int GetOpeningCurlyBraceOffsetForRegion(IDocument document)
        {
            int offset = minChar;
            while (offset < limChar) {
                if (document.GetCharAt(offset) == '{') {
                    return offset - 1;
                }
                ++offset;
            }

            return minChar;
        }

    }
}

