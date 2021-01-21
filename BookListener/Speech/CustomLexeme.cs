using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.Speech
{
    public class CustomLexeme
    {
        public string Grapheme { get; set; }
        public string PhoneticAlphabet { get; set; }
        public List<string> Phonemes { get; set; }
        public List<ProsodyRange> Prosodies { get; set; }
    }
}
