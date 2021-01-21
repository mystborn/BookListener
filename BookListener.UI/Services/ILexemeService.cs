using BookListener.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.UI.Services
{
    public interface ILexemeService
    {
        CustomLexicon GlobalLexicon { get; }
        IEnumerable<string> Lexicons { get; }

        CustomLexicon LoadCustomLexicon(string name);
        void SaveCustomLexicon(CustomLexicon lexicon);
    }
}
