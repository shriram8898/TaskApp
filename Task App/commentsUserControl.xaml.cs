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
using Task_App.Models;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.ObjectModel;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Task_App
{
    public sealed partial class commentsUserControl : UserControl
    {
        public comments comments { get { return this.DataContext as comments; } }
        public ObservableCollection<comments> com = new ObservableCollection<comments>();
        public commentsUserControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            string msgtime;
            DateTime d = DateTime.Now;
            string[] date = comments.dt.ToString().Split(' ');
            string time1 = comments.dt.ToShortTimeString();
            string day = date[0];
            if (day == d.ToString("MM/dd/yyyy"))
                msgtime = time1;
            else
                msgtime= comments.dt.ToShortDateString() + " "+ comments.dt.ToShortTimeString();
            com.Add(comments);
            string picture = "Assets/" + comments.empid + ".jpg";
            var bitmapImage = new BitmapImage(new Uri(this.BaseUri, picture));
            pic.ProfilePicture = bitmapImage;
            time.Text = msgtime;
        }
    }
}
