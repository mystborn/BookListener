using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.Speech
{
    public class ProsodyRange
    {
        public Prosody Prosody { get; set; }
        public int StartingPhonemeIndex { get; set; }
        public int EndingPhonemeIndex { get; set; }
    }
}
