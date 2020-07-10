using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Task_App.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Settings : Page
    {
        public Employee emp;
        public Settings()
        {
            
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            emp = e.Parameter as Employee;
            
        }

        private async void HandleCheck(object sender, RoutedEventArgs e)
        {
            string mes="hello";
            RadioButton rb = sender as RadioButton;
            if(rb.Name=="Dark")
            {
                App.Current.RequestedTheme = ApplicationTheme.Dark;
            }
            else if(rb.Name=="Light")
            {
                mes = "Light theme is selected";
            }
            else if(rb.Name=="Custom")
            {
                mes = "Custom theme is selected";
            }
            MessageDialog message = new MessageDialog(mes);
            await message.ShowAsync();
        }

        private async void HandleCheck1(object sender, RoutedEventArgs e)
        {
            string mes="Hello";
            RadioButton rb = sender as RadioButton;
            if (rb.Name == "Team")
            {
                mes = "You selected team view";
            }
            else if (rb.Name == "All")
            {
                mes = "You selected all view";
            }
            MessageDialog message = new MessageDialog(mes);
            await message.ShowAsync();
        }
    }
}
