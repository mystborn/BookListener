using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace BookListener.UI.Adorners
{
    public class AdornerPopup : AdornerContentPresenter
    {
        public AdornerPopup(UIElement adornedElement, Vector offset)
            : base(adornedElement)
        {
            Margin = new Thickness(offset.X, offset.Y, 0, 0);
        }

        public AdornerPopup(UIElement adornedElement, Visual content, Vector offset)
            : base(adornedElement, content)
        {
            Margin = new Thickness(offset.X, offset.Y, 0, 0);
        }

        /// <summary>
        /// Brings the popup into view.
        /// </summary>
        public void Show()
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(AdornedElement);
            adornerLayer.Add(this);
        }
        /// <summary>
        /// Removes the popup into view.
        /// </summary>
        public void Hide()
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(AdornedElement);
            adornerLayer.Remove(this);
        }
    }
}
