using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookListener.Extractors
{
    public static class ParagraphSeparators
    {
        public readonly static Regex DoubleNewLine = new Regex(@"(\n|\r\n)(\w*(\n|\r\n))+", RegexOptions.Singleline);
        public readonly static Regex SingleNewLine = new Regex(@"(\n|\r\n)+", RegexOptions.Singleline);
        public readonly static Regex NewLinePlusTab = new Regex(@"(\n|\r\n)+(\t|  )", RegexOptions.Singleline);
    }
}
