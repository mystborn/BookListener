using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookListener.Speech
{
    public partial class PhoneticAlphabet
    {
        private static PhoneticAlphabet _americanEnglish;

        public static PhoneticAlphabet AmericanEnglish
        {
            get
            {
                if (_americanEnglish is null)
                    _americanEnglish = LoadAlphabetFromManifest("x-microsoft-ups-american-english.xml");

                return _americanEnglish;
            }
        }

        private static PhoneticAlphabet LoadAlphabetFromManifest(string name)
        {
            using(var stream = typeof(PhoneticAlphabet).Assembly.GetManifestResourceStream(name))
            {
                using(var reader = new StreamReader(stream))
                {
                    var serializer = new XmlSerializer(typeof(PhoneticAlphabet));
                    return (PhoneticAlphabet)serializer.Deserialize(reader);
                }
            }
        }
    }
}
