using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookListener.Speech;
using BookListener.UI.Services;
using Prism;
using Prism.Commands;
using Prism.Mvvm;

namespace BookListener.UI.ViewModels
{
    public class LexemeBuilderViewModel : BindableBase
    {
        private string _grapheme;
        private PhoneticAlphabet _alphabet;
        private ISpeechService _speech;

        public string Grapheme
        {
            get => _grapheme;
            set => SetProperty(ref _grapheme, value);
        }

        public ObservableCollection<PhoneticAlphabet> PhonemeAlphabets { get; private set; } 

        public PhoneticAlphabet SelectedAlphabet
        {
            get => _alphabet;
            set
            {
                if(SetProperty(ref _alphabet, value))
                {
                    Phonemes.Clear();

                    PhonemeConsonants.Clear();
                    PhonemeVowels.Clear();
                    PhonemeProsodies.Clear();

                    PhonemeConsonants.AddRange(value.Consonants);
                    PhonemeVowels.AddRange(value.Vowels);
                    PhonemeProsodies.AddRange(value.Prosodies);
                }
            }
        }

        public ObservableCollection<Phoneme> PhonemeConsonants { get; private set; }
        public ObservableCollection<Phoneme> PhonemeVowels { get; private set; }
        public ObservableCollection<Phoneme> PhonemeProsodies { get; private set; }
        public ObservableCollection<Phoneme> Phonemes { get; } = new ObservableCollection<Phoneme>();

        public ICommand PlayGrapheme { get; }
        public ICommand PlayPhoneme { get; }

        public event EventHandler Saved;
        public event EventHandler Reset;

        public LexemeBuilderViewModel(ISpeechService speech)
        {
            _speech = speech;
            PlayGrapheme = new DelegateCommand(OnPlayGrapheme);
            PlayPhoneme = new DelegateCommand(OnPlayPhoneme);

            SelectedAlphabet = _speech.GetSupportedPhonemeAlphabets().First();
        }

        public void InitializeWithLexeme(CustomLexeme lexeme)
        {
            var alphabet = PhonemeAlphabets.FirstOrDefault(pa => pa.DisplayName == lexeme.PhoneticAlphabet);
            if (alphabet is null)
                throw new PhoneticAlphabetMissingException(lexeme.PhoneticAlphabet);

            var phonemes = lexeme.Phonemes.Select(p => FindPhonemeInAlphabet(p, alphabet)).ToList();

            SelectedAlphabet = alphabet;
            Grapheme = lexeme.Grapheme;
            Phonemes.AddRange(phonemes);
        }

        private Phoneme FindPhonemeInAlphabet(string phoneme, PhoneticAlphabet alphabet)
        {
            var result = alphabet.Consonants.Concat(alphabet.Vowels)
                                            .Concat(alphabet.Prosodies)
                                            .FirstOrDefault(p => p.Value == phoneme);

            if (result is null)
                throw new InvalidPhonemeException(phoneme, $"Couldn't find phoneme {phoneme} in alphabet {alphabet.DisplayName}");

            return result;
        }

        private void OnPlayGrapheme()
        {
            var value = GenerateSimpleSsml($"<s>{Grapheme}</s>");
            _speech.Speech.SpeakSsmlAsync(value);
        }

        private void OnPlayPhoneme()
        {
            var value = GenerateSimpleSsml(GeneratePhonemeString());
            _speech.Speech.SpeakSsml(value);
        }

        private string GeneratePhonemeString()
        {
            return $"<phoneme alphabet=\"{_alphabet.DisplayName}\" ph=\"{string.Join(_alphabet.Separator, Phonemes.Select(p => p.Value))}\">{Grapheme}</phoneme>";
        }

        private string GenerateSimpleSsml(string speechString)
        {
            return "<?xml version=\"1.0\"?>\n" +
                   "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\"\n" +
                   "       xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n" +
                   "       xsi:schemaLocation=\"http://www.w3.org/2001/10/synthesis\n" +
                   "                 http://www.w3.org/TR/speech-synthesis11/synthesis.xsd\"\n" +
                   "       xml:lang=\"en-US\">\n" +
                   "    " + speechString + "\n" +
                   "</speak>";
        }
    }
}
