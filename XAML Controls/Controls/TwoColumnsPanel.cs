using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace XAML_Controls.Controls
{
    public class TwoColumnsPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            var columnAvailableSize = new Size(availableSize.Width / 2, availableSize.Height);
            foreach (UIElement child in this.InternalChildren) {
                child.Measure(columnAvailableSize);
            }

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            int separator = 0;
            var leftLocation = new Point();
            double columnWidth = finalSize.Width / 2;
            var rightLocation = new Point(columnWidth, 0);

            foreach (UIElement child in this.InternalChildren) {
                bool isLeftColumn = separator % 2 == 0;
                child.Arrange(new Rect(
                    isLeftColumn ? leftLocation : rightLocation, 
                    child.DesiredSize));

                if (isLeftColumn)
                    leftLocation.Y += child.DesiredSize.Height;
                else
                    rightLocation.Y += child.DesiredSize.Height;

                separator++;
            }

            return finalSize;
        }
    }
}
