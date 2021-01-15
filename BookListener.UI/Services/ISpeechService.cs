using BookListener.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.UI.Services
{
    public interface ISpeechService
    {
        IEnumerable<PhoneticAlphabet> GetSupportedPhonemeAlphabets();
        SpeechSynthesizer Speech { get; }
    }
}
