using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.Speech
{
    public class InvalidPhonemeException : Exception
    {
        public Phoneme Phoneme { get; }

        public InvalidPhonemeException(string phoneme)
        {
            Phoneme = new Phoneme(PhonemeType.Unknown, phoneme, null, null);
        }

        public InvalidPhonemeException(Phoneme phoneme)
        {
            Phoneme = phoneme;
        }

        public InvalidPhonemeException(string phoneme, string message) 
            : base(message)
        {
            Phoneme = new Phoneme(PhonemeType.Unknown, phoneme, null, null);
        }

        public InvalidPhonemeException(Phoneme phoneme, string message)
            : base(message)
        {
            Phoneme = phoneme;
        }

        public InvalidPhonemeException(string phoneme, string message, Exception innerException) 
            : base(message, innerException)
        {
            Phoneme = new Phoneme(PhonemeType.Unknown, phoneme, null, null);
        }

        public InvalidPhonemeException(Phoneme phoneme, string message, Exception innerException)
            : base(message, innerException)
        {
            Phoneme = phoneme;
        }
    }
}
