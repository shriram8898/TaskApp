using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
        public ObservableCollection<td> tds = new ObservableCollection<td>();
        public TaskDetailedView()
        {
            
            this.InitializeComponent();
            
        }

        private async void initiaze()
        {
            await listIntializer();
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
            await upladFilesinDB();
        }

        private async Task listIntializer()
        {
            tds.Clear();
            string tableCommand = "SELECT * FROM task;";
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                while (reader.Read())
                {
                    tds.Add(new td
                    {
                        id = reader.GetString(0),
                        name = reader.GetString(1),
                        details = reader.GetString(2),
                        Assign_to = reader.GetString(3),
                        Assign_by = reader.GetString(4),
                        Assign_to_id = reader.GetString(5),
                        Assign_by_id = reader.GetString(6),
                        createdDate = reader.GetDateTime(8),
                        team = reader.GetString(7),
                        priority = reader.GetString(10),
                        status = reader.GetString(11),
                        collective = reader.GetString(12),
                        updated = reader.GetDateTime(13)
                    });
                }

                var groups = from c in tds
                             group c by c.collective;
                this.cvs.Source = groups;
                list.SelectedIndex = -1;
            }
        }

        private async Task upladFilesinDB()
        {
            files.Text = "";
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "SELECT * FROM colltask WHERE taskid='" + click.id + "';";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                if (reader.Read())
                {
                    files.Text = reader.GetString(2);
                    files.Visibility = Visibility.Visible;
                }
            }
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
                createTable.ExecuteReader();
                tableCommand = "DELETE FROM comment WHERE taskid='" + click.id + "';";
                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
                tableCommand = "DELETE FROM colltask WHERE taskid='" + click.id + "';";
                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
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

        private async void upload_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            StringBuilder output = new StringBuilder();
            if (file != null)
            {
                files.Text = "";
                output.Append("File Name   :"+file.Name + "\n");
                output.Append("File Created:"+file.DateCreated);
                files.Text = output.ToString();
                files.Visibility = Visibility.Visible;
                await WriteDataDatabase(output);
            }

        }

        private async Task WriteDataDatabase(StringBuilder output)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "INSERT INTO colltask(taskid,name)" +
                    "VALUES('" + click.id + "','" + output.ToString() + "');";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

            }
        }


        // C# CODE FOR EDIT CONTENT DIALOG
        private async void initializeData()
        {

            priority1.Items.Add("None");
            priority1.Items.Add("Low");
            priority1.Items.Add("Medium");
            priority1.Items.Add("High");
            collective1.Items.Add("sample");
            collective1.Items.Add("Login");
            collective1.Items.Add("Other");
            status.Items.Add("Open");
            status.Items.Add("Close");
            await LoadDataToForms();
        }

        private async Task LoadDataToForms()
        {
            int count = 0;
            taskName1.Text = click.name;
            taskDetails1.Text = click.details;
            foreach (var item in priority1.Items)
            {
                if (item.ToString() == click.priority)
                {
                    priority1.SelectedIndex = count; break;
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
            foreach (var item in collective1.Items)
            {
                if (item.ToString() == click.collective)
                {
                    collective1.SelectedIndex = count; break;
                }
                else
                    count++;
            }


        }



        private async void save_Click(object sender, RoutedEventArgs e)
        {
            string name = taskName1.Text;
            string details = taskDetails1.Text;
            string prior = priority1.SelectedItem.ToString();

            string coll = collective1.SelectedItem.ToString();

            if (name == "" || details == "" || coll == "")
                return;
            else
            {
                await writeinDb(name, details, prior, coll);
                
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
            await listIntializer();
            Frame.Navigate(typeof(TaskDetailedView), data);
        }

        private void edit1_Click(object sender, RoutedEventArgs e)
        {
            taskDetails1.Visibility = Visibility.Visible;
        }

        private void cancel1_Click(object sender, RoutedEventArgs e)
        {
            contentDialog2.Hide();
        }

        private void list_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem;
            click = (td)clickedItem;
            initiaze();
        }
    }
}
