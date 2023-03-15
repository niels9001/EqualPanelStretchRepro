using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace EqualPanelStretchRepro
{
    public partial class EqualPanel : Panel
    {
        private double maxItemWidth = 0;
        private double maxItemHeight = 0;

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        /// <summary>
        /// Identifies the Spacing dependency property.
        /// </summary>
        /// <returns>The identifier for the <see cref="Spacing"/> dependency property.</returns>
        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
            nameof(Spacing),
            typeof(double),
            typeof(EqualPanel),
            new PropertyMetadata(default(double), OnSpacingChanged));

        public EqualPanel()
        {
            RegisterPropertyChangedCallback(HorizontalAlignmentProperty, OnHorizontalAlignmentChanged);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            maxItemWidth = 0;
            maxItemHeight = 0;

            if (Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    child.Measure(availableSize);
                    maxItemWidth = Math.Max(maxItemWidth, child.DesiredSize.Width);
                    maxItemHeight = Math.Max(maxItemHeight, child.DesiredSize.Height);
                }

                // Return equal widths based on the widest item
                // Or, when part of a Grid.Column the Infinite might be 
                if (HorizontalAlignment != HorizontalAlignment.Stretch)
                {
                    return new Size((maxItemWidth * Children.Count) + (Spacing * (Children.Count - 1)), maxItemHeight);
                }
                else
                {
                    ////////////////////////////
                    ////////////////////////////
                    ////////////////////////////

                    // The available width is infinite here when the control is in a Grid.Column which is set to Auto

                    if (double.IsInfinity(availableSize.Width))
                    {
                        var x = availableSize.Width;
                        // x = infinite here.. how to get the available space?
                        // For now, we do the same as Left/Right/Center alignment = but ideally we run the code that is in LINE 75.
                        return new Size((maxItemWidth * Children.Count) + (Spacing * (Children.Count - 1)), maxItemHeight);
                    }

                    // Equal columns based on the available width, adjust for spacing
                    double totalWidth = availableSize.Width - (Spacing * (Children.Count - 1));
                    maxItemWidth = totalWidth / Children.Count;
                    return new Size(availableSize.Width, maxItemHeight);
                }
            }
            else
            {
                return new Size(0, 0);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = 0.0;
            foreach (var child in Children)
            {
                child.Arrange(new Rect(x, 0, maxItemWidth, maxItemHeight));
                x += maxItemWidth + Spacing;
            }
            return finalSize;
        }
        private void OnHorizontalAlignmentChanged(DependencyObject sender, DependencyProperty dp)
        {
            InvalidateMeasure();
        }

        private static void OnSpacingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = (EqualPanel)d;
            panel.InvalidateMeasure();
        }
    }

}