using BookListener.Speech;
using BookListener.UI.Adorners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookListener.UI.Views
{
    /// <summary>
    /// Interaction logic for PhonemeControl.xaml
    /// </summary>
    public partial class PhonemeControl : UserControl
    {
        private AdornerPopup _tooltip;

        public PhonemeControl(Phoneme phoneme)
        {
            InitializeComponent();
            ((ViewModels.PhonemeControlViewModel)DataContext).Phoneme = phoneme;

            var tooltip = new PhonemeTooltip();
            tooltip.DataContext = DataContext;

            _tooltip = new AdornerPopup(Outline, tooltip, new Vector(Height + 10, Width / 2));
        }

        private void Outline_MouseEnter(object sender, MouseEventArgs e)
        {
            _tooltip.Show();
        }

        private void Outline_MouseLeave(object sender, MouseEventArgs e)
        {
            _tooltip.Hide();
        }
    }
}
