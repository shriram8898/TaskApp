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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskDetailedView : Page
    {
        public Employee emp = new Employee();
        public td click = new td();
        public PasData data;
        public TaskDetailedView()
        {
            
            this.InitializeComponent();
            
        }

        private async void initiaze()
        {
            if(!(click.Assign_by_id==emp.id||emp.post=="manager"))
            {
                delete.Visibility = Visibility.Collapsed;
                edit.Visibility = Visibility.Collapsed;
            }
            if (click.Assign_to_id == emp.id)
            {
                complete.Visibility = Visibility.Visible;
            }
                
            tName.Text =click.name;
            tid.Text = click.id;
            tby.Text = click.Assign_by + "(" + click.Assign_by_id + ")"; 
            tto.Text = click.Assign_to + "(" + click.Assign_to_id + ")";
            td.Text = click.details;
            tstatus.Text =click.status;
            tprior.Text = click.priority;
            tup.Text = click.updated.ToString();
            trelate.Text = click.collective;
            tcreated.Text = click.createdDate.ToString();
            tName.Foreground = new SolidColorBrush(Colors.Black);
            if (click.priority == "High")
            {
                tprior.Foreground = new SolidColorBrush(Colors.Red);                
            }                
            else if (click.priority == "Medium")
            {
                tprior.Foreground = new SolidColorBrush(Colors.Gray);
            }  
            else if (click.priority == "Low")
            {
                tprior.Foreground = new SolidColorBrush(Colors.SeaGreen);
            }
                
            await DisplayTaskDetails();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            data = e.Parameter as PasData;
            emp = data.emp;
            click = data.click1;
            initiaze();
        }
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Display), emp);
        }

        private void commSec_Click(object sender, RoutedEventArgs e)
        {
            if (commen.Visibility == Visibility.Visible)
            {
                commen.Visibility = Visibility.Collapsed;
            }
                
            else
            {
                commen.Visibility = Visibility.Visible;
            }
                
        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            await deleteFromDb();
            Frame.Navigate(typeof(Display), emp);

        }

        private async Task deleteFromDb()
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "DELETE FROM task WHERE taskid='" + click.id + "';";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                while (reader.Read())
                {
                    comments.Items.Add(reader.GetString(1));
                }
            }
        }
        private async Task DisplayTaskDetails()
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "SELECT * FROM comment WHERE taskid='" + click.id + "' ;";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                while (reader.Read())
                {
                    comments.Items.Add(reader.GetString(1));
                }
                if(comments.Items.Count==0)
                {
                    comments.Items.Add("No Comments");
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string mess = emp.name + ":" + combox.Text;
            combox.Text = "";
            comments.Items.Remove("No Comments");
            comments.Items.Add(mess);
            await writeInDb(mess);
        }

        private async Task writeInDb(string mess)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "INSERT INTO comment(taskid,comments)" +
                    "VALUES('" + click.id + "','" + mess + "');";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                while (reader.Read())
                {
                    comments.Items.Add(reader.GetString(1));
                }
            }
        }

        private async void edit_Click(object sender, RoutedEventArgs e)
        {
            initializeData();
            //contentDialog2.Title = "Edit Task";
            await contentDialog2.ShowAsync();
        }
        private void dialog_createClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Frame.Navigate(typeof(TaskDetailedView), data);
        }

        private async void complete_Click(object sender, RoutedEventArgs e)
        {
            complete.Visibility = Visibility.Visible;
            await editindb();
        }

        private async Task editindb()
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "UPDATE task SET status='Close' WHERE taskid='" + click.id + "'";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }




        // C# CODE FOR EDIT CONTENT DIALOG
        private async void initializeData()
        {

            priority.Items.Add("None");
            priority.Items.Add("Low");
            priority.Items.Add("Medium");
            priority.Items.Add("High");
            collective.Items.Add("sample");
            collective.Items.Add("Login");
            collective.Items.Add("Other");
            status.Items.Add("Open");
            status.Items.Add("Close");
            await LoadDataToForms();
        }

        private async Task LoadDataToForms()
        {
            int count = 0;
            taskName.Text = click.name;
            taskDetails.Text = click.details;
            foreach (var item in priority.Items)
            {
                if (item.ToString() == click.priority)
                {
                    priority.SelectedIndex = count; break;
                }
                else
                    count++;
            }
            count = 0;
            foreach (var item in status.Items)
            {
                if (item.ToString() == click.status)
                {
                    status.SelectedIndex = count; break;
                }
                else
                    count++;
            }
            count = 0;
            foreach (var item in collective.Items)
            {
                if (item.ToString() == click.collective)
                {
                    collective.SelectedIndex = count; break;
                }
                else
                    count++;
            }


        }



        private async void add_Click(object sender, RoutedEventArgs e)
        {
            string name = taskName.Text;
            string details = taskDetails.Text;
            string prior = priority.SelectedItem.ToString();

            string coll = collective.SelectedItem.ToString();

            if (name == "" || details == "" || coll == "")
                return;
            else
            {
                await writeinDb(name, details, prior, coll);
                taskName.Text = "";
                taskDetails.Text = "";
                priority.SelectedIndex = 0;
                collective.SelectedIndex = 2;

            }
        }

        private async Task writeinDb(string name, string details, string prior, string coll)
        {
            await WriteInDB(name, details, prior, coll);
        }
        private async Task WriteInDB(string name, string details, string prior, string coll)
        {
            string status = "Open";
            var dt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
            click.updated = DateTime.Now;click.name = name;click.details = details;click.priority = prior;click.status = status;click.collective = coll;
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "UPDATE task SET updated='" + dt + "',taskname='" + name + "',taskdetails='" + details + "',priority='" + prior + "',status='" + status + "',collective='" + coll + "' WHERE taskid='" + click.id + "'";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

            }
            contentDialog2.Hide();
            data.click1 = click;
            Frame.Navigate(typeof(TaskDetailedView), data);
        }

        private void edit1_Click(object sender, RoutedEventArgs e)
        {
            taskDetails.Visibility = Visibility.Visible;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            contentDialog2.Hide();
        }

       
    }
}
