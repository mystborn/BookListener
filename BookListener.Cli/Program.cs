using BookListener.Speech;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookListener.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var vowels = new List<Phoneme>()
            {
                new Phoneme(PhonemeType.Vowel, "AA", new PhonemeExample("p", "o", "t"), "P AA T"),
                new Phoneme(PhonemeType.Vowel, "AE", new PhonemeExample("f", "a", "t"), "F AE T"),
                new Phoneme(PhonemeType.Vowel, "AH", new PhonemeExample("b", "u", "g"), "B AH G"),
                new Phoneme(PhonemeType.Vowel, "AI", new PhonemeExample("h", "i", "ve"), "H AI V"),
                new Phoneme(PhonemeType.Vowel, "AU", new PhonemeExample("", "ou", "t"), "AU T"),
                new Phoneme(PhonemeType.Vowel, "AO", new PhonemeExample("c", "augh", "t"), "K AO T"),
                new Phoneme(PhonemeType.Vowel, "AX", new PhonemeExample("driv", "e", "r"), "D RA AI V AX RA"),
                new Phoneme(PhonemeType.Vowel, "AX RA", new PhonemeExample("runn", "er", ""), "RA AH N AX RA"),
                new Phoneme(PhonemeType.Vowel, "EH", new PhonemeExample("", "e", "gg"), "EH G"),
                new Phoneme(PhonemeType.Vowel, "EH RA", new PhonemeExample("h", "air", ""), "H EH RA"),
                new Phoneme(PhonemeType.Vowel, "EI", new PhonemeExample("l", "a", "ke"), "L EI K"),
                new Phoneme(PhonemeType.Vowel, "ER", new PhonemeExample("h", "er", ""), "H ER RA"),
                new Phoneme(PhonemeType.Vowel, "I", new PhonemeExample("m", "ea", "l"), "M I L"),
                new Phoneme(PhonemeType.Vowel, "IH", new PhonemeExample("", "i", "nk"), "IH NG K"),
                new Phoneme(PhonemeType.Vowel, "O + UH", new PhonemeExample("r", "o", "se"), "RA O + UH Z"),
                new Phoneme(PhonemeType.Vowel, "OI", new PhonemeExample("t", "oy", ""), "T OI"),
                new Phoneme(PhonemeType.Vowel, "U", new PhonemeExample("f", "oo", "d"), "F U D"),
                new Phoneme(PhonemeType.Vowel, "UH", new PhonemeExample("l", "oo", "k"), "L UH K")
            };

            var consonants = new List<Phoneme>()
            {
                new Phoneme(PhonemeType.Consonant, "AX L", new PhonemeExample("whis", "tle", ""), "W IH S AX L"),
                new Phoneme(PhonemeType.Consonant, "AX M", new PhonemeExample("alb", "um", ""), "AE L B AX M"),
                new Phoneme(PhonemeType.Consonant, "AX N", new PhonemeExample("butt", "on", ""), "B AH T AX N"),
                new Phoneme(PhonemeType.Consonant, "B", new PhonemeExample("", "b", "in"), "B IH N"),
                new Phoneme(PhonemeType.Consonant, "CH", new PhonemeExample("", "ch", "in"), "CH IH N"),
                new Phoneme(PhonemeType.Consonant, "D", new PhonemeExample("", "d", "ate"), "D EI T"),
                new Phoneme(PhonemeType.Consonant, "DH", new PhonemeExample("", "th", "is"), "DH IH S"),
                new Phoneme(PhonemeType.Consonant, "F", new PhonemeExample("", "f", "ax"), "F AE K S"),
                new Phoneme(PhonemeType.Consonant, "G", new PhonemeExample("", "g", "ap"), "G AE P"),
                new Phoneme(PhonemeType.Consonant, "H", new PhonemeExample("", "h", "elp"), "H EH L P"),
                new Phoneme(PhonemeType.Consonant, "J", new PhonemeExample("", "y", "acht"), "J AA T"),
                new Phoneme(PhonemeType.Consonant, "JH", new PhonemeExample("", "g", "in"), "JH IH N"),
                new Phoneme(PhonemeType.Consonant, "K", new PhonemeExample("", "k", "ing"), "K IH NG"),
                new Phoneme(PhonemeType.Consonant, "L", new PhonemeExample("", "l", "eg"), "L EH G"),
                new Phoneme(PhonemeType.Consonant, "M", new PhonemeExample("", "m", "ail"), "M EI L"),
                new Phoneme(PhonemeType.Consonant, "N", new PhonemeExample("", "n", "ose"), "N O + UH Z"),
                new Phoneme(PhonemeType.Consonant, "NG", new PhonemeExample("lo", "ng", ""), "L AO NG"),
                new Phoneme(PhonemeType.Consonant, "P", new PhonemeExample("", "p", "in"), "P IH N"),
                new Phoneme(PhonemeType.Consonant, "RA", new PhonemeExample("", "r", "ing"), "RA IH NG"),
                new Phoneme(PhonemeType.Consonant, "S", new PhonemeExample("", "s", "ing"), "S IH NG"),
                new Phoneme(PhonemeType.Consonant, "SH", new PhonemeExample("", "sh", "op"), "SH AA P"),
                new Phoneme(PhonemeType.Consonant, "T", new PhonemeExample("", "t", "ip"), "T IH P"),
                new Phoneme(PhonemeType.Consonant, "TH", new PhonemeExample("", "th", "ing"), "TH IH NG"),
                new Phoneme(PhonemeType.Consonant, "V", new PhonemeExample("", "v", "ery"), "V EH RA I"),
                new Phoneme(PhonemeType.Consonant, "W", new PhonemeExample("", "w", "ill"), "W IH L"),
                new Phoneme(PhonemeType.Consonant, "Z", new PhonemeExample("", "z", "ip"), "Z IH P"),
                new Phoneme(PhonemeType.Consonant, "ZH", new PhonemeExample("vi", "si", "on"), "V IH ZH AX N"),
            };

            var prosodies = new List<Phoneme>()
            {
                new Phoneme(PhonemeType.Prosody, "S1", new PhonemeExample("", "s", "tress"), "S1 S T RA EH S", "primary stress"),
                new Phoneme(PhonemeType.Prosody, "S2", new PhonemeExample("secon", "d", "ary"), "S1 S EH . K AX N . S2 D EH . RA . I", "secondary stress"),
                new Phoneme(PhonemeType.Prosody, ".", new PhonemeExample("vi", "-", "sion"), "S1 V IH . ZH AX N", "syllable break"),
            };

            var phonemeAlphabet = new PhoneticAlphabet("American English (UPS)", "x-microsoft-ups", " ", consonants, vowels, prosodies);
            using(var writer = new StreamWriter(@"C:\Users\Chris\Source\Repos\BookListener\BookListener\Speech\PhonemeAlphabets\x-microsoft-ups-american-english.xml"))
            {
                var serializer = new XmlSerializer(typeof(PhoneticAlphabet));
                serializer.Serialize(writer, phonemeAlphabet);
            }

            using (var reader = new StreamReader(@"C:\Users\Chris\Source\Repos\BookListener\BookListener\Speech\PhonemeAlphabets\x-microsoft-ups-american-english.xml"))
            {
                var serializer = new XmlSerializer(typeof(PhoneticAlphabet));
                var alphabet = (PhoneticAlphabet)serializer.Deserialize(reader);
            }

            Console.WriteLine("Successfully serialized");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            /*
            using(var speech = new SpeechSynthesizer())
            {
                speech.SetOutputToDefaultAudioDevice();
                speech.SelectVoiceByHints(VoiceGender.Female);

                var str = "<?xml version=\"1.0\"?>\n" +
                          "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\"\n" +
                          "       xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n" +
                          "       xsi:schemaLocation=\"http://www.w3.org/2001/10/synthesis\n" +
                          "                 http://www.w3.org/TR/speech-synthesis11/synthesis.xsd\"\n" +
                          "       xml:lang=\"en-US\">\n" +
                          "  <prosody contour=\"(0%,+20%) (20%,-40%) (100%,+20%)\" rate=\"0.8\"> <phoneme alphabet=\"x-microsoft-ups\" ph=\" . S1 J I \"> ye </phoneme></prosody>\n" +
                          "  <phoneme alphabet=\"x-microsoft-ups\" ph=\" . S1 W AO N . W AO N\"> wanwan </phoneme> is here to party!\n" +
                          "</speak>";

                speech.BookmarkReached += (s, e) =>
                {
                    Console.WriteLine($"Bookmark Reached: {e.Bookmark}");
                };

                speech.SpeakSsmlAsync(str);
                Console.ReadLine();
            }
            */
        }
    }
}
