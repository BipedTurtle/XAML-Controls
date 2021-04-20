using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace XAML_Controls.Controls
{
    public class TwoColumnsPanel : Panel
    {
        public TwoColumnsPanel()
        {
            Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        public static bool GetBelongsToRight(DependencyObject obj)
        {
            return (bool)obj.GetValue(BelongsToRightProperty);
        }

        public static void SetBelongsToRight(DependencyObject obj, bool value)
        {
            obj.SetValue(BelongsToRightProperty, value);
        }

        public static readonly DependencyProperty BelongsToRightProperty =
            DependencyProperty.RegisterAttached("BelongsToRight", typeof(bool), typeof(TwoColumnsPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsParentArrange));

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
            var leftLocation = new Point();
            double columnWidth = finalSize.Width / 2;
            var rightLocation = new Point(columnWidth, 0);

            foreach (UIElement child in this.InternalChildren) {
                bool isRight = (bool)child.GetValue(BelongsToRightProperty);
                child.Arrange(new Rect(
                    isRight ? rightLocation : leftLocation, 
                    child.DesiredSize));

                if (isRight)
                    rightLocation.Y += child.DesiredSize.Height;
                else
                    leftLocation.Y += child.DesiredSize.Height;
            }

            return finalSize;
        }
    }
}