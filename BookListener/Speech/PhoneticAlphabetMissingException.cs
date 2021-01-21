using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.Speech
{
    public class PhoneticAlphabetMissingException : Exception
    {
        public string PhoneticAlphabetName { get; }

        public PhoneticAlphabetMissingException(string phoneticAlphabetName)
            : base($"Couldn't find an alphabet called {phoneticAlphabetName}")
        {
            PhoneticAlphabetName = phoneticAlphabetName;
        }

        public PhoneticAlphabetMissingException(string phoneticAlphabetName, string message) 
            : base(message)
        {
            PhoneticAlphabetName = phoneticAlphabetName;
        }

        public PhoneticAlphabetMissingException(string phoneticAlphabetName, string message, Exception innerException) 
            : base(message, innerException)
        {
            PhoneticAlphabetName = phoneticAlphabetName;
        }
    }
}
