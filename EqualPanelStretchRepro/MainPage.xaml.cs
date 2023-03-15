using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EqualPanelStretchRepro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (AlignmentBox.SelectedIndex)
            {
                case 0:
                    NormalListView.HorizontalAlignment= HorizontalAlignment.Left;
                    BuggyListView.HorizontalAlignment= HorizontalAlignment.Left;
                    break;
                case 1:
                    NormalListView.HorizontalAlignment = HorizontalAlignment.Right;
                    BuggyListView.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
                case 2:
                    NormalListView.HorizontalAlignment = HorizontalAlignment.Center;
                    BuggyListView.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case 3:
                    NormalListView.HorizontalAlignment = HorizontalAlignment.Stretch;
                    BuggyListView.HorizontalAlignment = HorizontalAlignment.Stretch;
                    break;

            }
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            AlignmentBox.SelectedIndex = 0; 
        }
    }
}
