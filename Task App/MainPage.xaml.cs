using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Task_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            user.Text = "shri8858@gmail.com";
            pass.Text = "shriram8898";
            InitializeComponent1();
            
        }
        public Employee emp;
        private async void InitializeComponent1()
        {
            await StartCreateAsync();

        }

        private async Task StartCreateAsync()
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS task (taskid VARCHAR(2048),taskname VARCHAR(2048),taskdetails VARCHAR(2048),assignedto VARCHAR(2048),assignedby VARCHAR(2048),assignedtoId VARCHAR(2048),assignedbyId VARCHAR(2048),team VARCHAR(2048),updated DATE,createdDate DATE,completedDate DATE,priority VARCHAR(2048),status VARCHAR(2048),collective VARCHAR(2048))";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                tableCommand = "CREATE TABLE IF NOT EXISTS comment (taskid NVARCHAR(2048),comments NVARCHAR(2048))";
                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
                tableCommand = "CREATE TABLE IF NOT EXISTS emp (empid NVARCHAR(2048),empname NVARCHAR(2048),team NVARCHAR(2048),jobPost NVARCHAR(2048),email NVARCHAR(2048),password NVARCHAR(2048))";
                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
                tableCommand = "CREATE TABLE IF NOT EXISTS files (id NVARCHAR(2048),taskid NVARCHAR(2048),name NVARCHAR(2048))";
                createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                createTable.ExecuteReader();
                
            }
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            string us = user.Text;
            string pas = pass.Text;
            if (us == "" || pas == "")
            {
                return;
            }
            else
            {
                emp = new Employee { id = "001", name = "Shriram", post = "developer", username = "123@gmail.com", password = "123", team = "desktop" };
                Frame.Navigate(typeof(DashBoard), emp);
                //checkDb(us, pas);
            }
        }

        private void checkDb(string us, string pass)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "SELECT * FROM emp WHERE email='" + us + "' AND password='" + pass + "';";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                if (reader.Read())
                {
                    //emp = new Employee { id = "001", name = "Shriram", post = "developer", username = "123@gmail.com", password = "123", team = "desktop" };
                    emp = new Employee { id = reader.GetString(0), name = reader.GetString(1), post = reader.GetString(3), username = reader.GetString(4), password = reader.GetString(5), team = reader.GetString(2) };
                    Frame.Navigate(typeof(DashBoard), emp);
                }

            }
        }



    }
}
