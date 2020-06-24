using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterUC : Page
    {
        public td td { get { return this.DataContext as td; } }
        public MasterUC()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

        private async void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Today;
            string[] spli = dt.ToString().Split(' ');
            string today = spli[0];
            dt = dt.AddDays(-1);
            spli = dt.ToString().Split(' ');
            string yesterday = spli[0];
            string[] time = td.createdDate.ToString().Split(' ');
            if (time[0] == today)
                date1.Text = "Today";
            else if (time[0] == yesterday)
                date1.Text = "Yesterday";
            else
                date1.Text = td.createdDate.ToString("MMMM dd");
            if (td.priority == "High")
            {
                Priority.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (td.priority == "Medium")
            {
                Priority.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else if (td.priority == "Low")
            {
                Priority.Foreground = new SolidColorBrush(Colors.SeaGreen);
            }
            else
                Priority.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
