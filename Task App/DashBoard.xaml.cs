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
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashBoard : Page
    {
        public Employee emp;
        public DashBoard()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            emp = e.Parameter as Employee;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
        }

        private async void SplitViewItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Task.IsSelected)
            {
                myframe.Navigate(typeof(TaskList), emp);
            }
            else if(Teams.IsSelected)
            {
                myframe.Navigate(typeof(TeamDetails), emp);
            }
            else if(Members.IsSelected)
            {
                myframe.Navigate(typeof(MembersDetails), emp);
            }
            else if(Settings.IsSelected)
            {
                myframe.Navigate(typeof(Settings), emp);
            }
            else if(Logout.IsSelected)
            {
                Frame.Navigate(typeof(MainPage));
            }
        }

       
    }
}
