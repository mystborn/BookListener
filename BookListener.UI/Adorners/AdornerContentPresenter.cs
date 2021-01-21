using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace BookListener.UI.Adorners
{
    /// <summary>
    /// An <see cref="Adorner"/> that can draw arbitrary visual elements.
    /// </summary>
    /// <remarks>
    /// Source and explanation for this class can be found here:
    /// https://web.archive.org/web/20130126075829/http://www.switchonthecode.com/tutorials/wpf-tutorial-using-a-visual-collection
    /// 
    /// Unfortunately the original site is down, so that is an archive of the original page.
    /// </remarks>
    public class AdornerContentPresenter : Adorner
    {
        private VisualCollection _visuals;
        private ContentPresenter _contentPresenter;

        public AdornerContentPresenter(UIElement adornedElement)
          : base(adornedElement)
        {
            _visuals = new VisualCollection(this);
            _contentPresenter = new ContentPresenter();
            _visuals.Add(_contentPresenter);
        }

        public AdornerContentPresenter(UIElement adornedElement, Visual content)
          : this(adornedElement)
        { Content = content; }

        protected override Size MeasureOverride(Size constraint)
        {
            _contentPresenter.Measure(constraint);
            return _contentPresenter.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _contentPresenter.Arrange(new Rect(0, 0,
                 finalSize.Width, finalSize.Height));
            return _contentPresenter.RenderSize;
        }

        protected override Visual GetVisualChild(int index)
        { return _visuals[index]; }

        protected override int VisualChildrenCount
        { get { return _visuals.Count; } }

        public object Content
        {
            get { return _contentPresenter.Content; }
            set { _contentPresenter.Content = value; }
        }
    }
}
