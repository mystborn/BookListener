using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookListener.UI.ViewModels
{
    public class AddNewLexemeViewModel : BindableBase
    {
        public ICommand AddNewLexemeRequest { get; }

        public event EventHandler AddNewLexemeRequested;

        public AddNewLexemeViewModel()
        {
            AddNewLexemeRequest = new DelegateCommand(OnAddNewLexemeRequest);
        }

        private void OnAddNewLexemeRequest()
        {
            AddNewLexemeRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
