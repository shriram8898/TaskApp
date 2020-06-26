using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Task_App
{
    public sealed partial class DetailsOfList : UserControl
    {
        //public td td { get { return this.DataContext as td; } }
        public DetailsOfList()
        {
            this.InitializeComponent();
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
           
                six.Foreground = new SolidColorBrush(Colors.White);
                if (six.Text == "High")
                    six1.Background = new SolidColorBrush(Colors.Red);
                else if (six.Text == "Medium")
                    six1.Background = new SolidColorBrush(Colors.Gray);
                else if (six.Text == "Low")
                    six1.Background = new SolidColorBrush(Colors.SeaGreen);
                else if (six.Text == "None")
                    six.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
