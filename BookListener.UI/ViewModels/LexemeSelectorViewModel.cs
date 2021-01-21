using BookListener.Speech;
using BookListener.UI.Services;
using BookListener.UI.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookListener.UI.ViewModels
{
    public class LexemeSelectorViewModel : BindableBase
    {
        private string _lexicon;
        private ILexemeService _lexemeService;
        private Control _displayControl;
        private int _selectedLexemeIndex;

        public string Lexicon
        {
            get => _lexicon;
            set
            {
                if (_lexicon == value)
                    return;

                try
                {
                    if (HasSelectedLexemeChanged() && !DiscardLexemeChanges())
                        return;

                    var lexicon = _lexemeService.LoadCustomLexicon(value);

                    SetProperty(ref _lexicon, value);

                    Lexemes.Clear();
                    Lexemes.AddRange(lexicon.Lexemes);
                }
                catch (Exception e)
                {
                    var message = string.Format(Strings.LexemeSelector_FailedToLoadLexicon, value, e);
                    MessageBox.Show(message, Strings.Error, MessageBoxButton.OK);
                }
            }
        }

        public ObservableCollection<CustomLexeme> Lexemes { get; } = new ObservableCollection<CustomLexeme>();

        public Control DisplayControl
        {
            get => _displayControl;
            set => SetProperty(ref _displayControl, value);
        }

        public int SelectedLexemeIndex
        {
            get => _selectedLexemeIndex;
            set
            {
                if (_selectedLexemeIndex == value)
                    return;

                if(_selectedLexemeIndex >= Lexemes.Count || _selectedLexemeIndex < -1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                SetProperty(ref _selectedLexemeIndex, value);

                if(_selectedLexemeIndex == -1)
                {
                    var addNewLexeme = new AddNewLexeme();
                    var addNewLexemeVM = (AddNewLexemeViewModel)addNewLexeme.DataContext;

                    addNewLexemeVM.AddNewLexemeRequested += (s, e) =>
                    {
                        
                    }
                }
                else
                {
                    var lexemeBuilder = new LexemeBuilder();
                    var lexemeBuilderVM = (LexemeBuilderViewModel)lexemeBuilder.DataContext;
                    var lexeme = Lexemes[value];

                    lexemeBuilderVM.InitializeWithLexeme(lexeme);
                    lexemeBuilderVM.Saved += (s, e) =>
                    {

                    };

                    lexemeBuilderVM.Reset += (s, e) =>
                    {

                    };

                    DisplayControl = lexemeBuilder;
                }
            }
        }

        public LexemeSelectorViewModel(ILexemeService lexemeService)
        {
            _lexemeService = lexemeService;
        }

        private bool DiscardLexemeChanges()
        {
            var result = MessageBox.Show(
                Strings.LexemeSelector_DiscardLexemeChanges,
                Strings.LexemeSelector_DiscardLexemeChangesTitle,
                MessageBoxButton.YesNo);

            return result == MessageBoxResult.Yes;
        }

        private bool HasSelectedLexemeChanged()
        {
            var lexemeBuilder = DisplayControl as LexemeBuilder;
            if (lexemeBuilder is null)
                return false;

            var lexemeBuilderVM = (LexemeBuilderViewModel)lexemeBuilder.DataContext;
            var originalLexeme = Lexemes[_selectedLexemeIndex];

            return originalLexeme.PhoneticAlphabet == lexemeBuilderVM.SelectedAlphabet.DisplayName &&
                   originalLexeme.Phonemes.SequenceEqual(lexemeBuilderVM.Phonemes.Select(p => p.Value));
        }
    }
}
