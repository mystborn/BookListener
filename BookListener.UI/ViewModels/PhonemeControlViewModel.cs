using BookListener.Speech;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.UI.ViewModels
{
    public class PhonemeControlViewModel : BindableBase
    {
        private Phoneme _phoneme;

        public Phoneme Phoneme
        {
            get => _phoneme;
            set => SetProperty(ref _phoneme, value);
        }
    }
}
