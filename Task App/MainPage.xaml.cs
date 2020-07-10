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
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Task_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Employee emp;
        public MainPage()
        {
            //Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] = Colors.Red;
            this.InitializeComponent();
            this.InitializeComponent();
            user.Text = "shri8858@gmail.com";
            pass.Text = "shriram8898";
            emp = new Employee();
        }
        private async void login_Click(object sender, RoutedEventArgs e)
        {
            string us = user.Text;
            string pas = pass.Text;
            if (us == "" || pas == "")
            {
                return;
            }
            else
            {
                string tableCommand = "SELECT * FROM emp WHERE email='" + us + "' AND password='" + pas + "';";
                emp =await DataBase.VerifyDatabase(tableCommand);
                if (emp.id!=null)
                {
                    emp =await DataBase.VerifyDatabase(tableCommand);
                    Frame.Navigate(typeof(DashBoard),emp);
                }
            }
        }
    }
}