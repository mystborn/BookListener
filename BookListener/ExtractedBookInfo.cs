using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookListener
{
    public class ExtractedBookInfo
    {
        /// <summary>
        /// The general information about the book.
        /// </summary>
        public BookInfo Info { get; set; }

        /// <summary>
        /// The location of the book file.
        /// </summary>
        public string OriginalDocument { get; set; }

        /// <summary>
        /// A plain-text document containing the extracted paragraphs from the book.
        /// </summary>
        public string PlainText { get; set; }

        /// <summary>
        /// An SSML file that can be used to listen to the book using a <see cref="System.Speech.Synthesis.SpeechSynthesizer"/>.
        /// </summary>
        public string SsmlText { get; set; }

        /// <summary>
        /// A Pronunciation Lexicon Specification (.pls) file that describes how certain words/phrases specific to this book should be pronounced.
        /// </summary>
        public string Lexicon { get; set; }
    }
}
