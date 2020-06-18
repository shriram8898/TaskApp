using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.AllJoyn;
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
    public sealed partial class Display : Page
    {
        public ObservableCollection<td> tds = new ObservableCollection<td>();
        public PasData data;
        public Employee emp;
        public td click;
        public Display()
        {
            this.InitializeComponent();
            data = new PasData();
            select.Items.Add("All");
            select.Items.Add("Assigned by me");
            select.Items.Add("Assigned to me");
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            emp = e.Parameter as Employee;
        }

        private async void select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            star2.Visibility = Visibility.Collapsed;
            stark1.Visibility = Visibility.Visible;
            string tableCommand; 
            if (select.SelectedItem.ToString()=="All")
            {
                tableCommand = "SELECT * FROM task;";
            }
            else if(select.SelectedItem.ToString()=="Assigned by me")
            {
                tableCommand = "SELECT * FROM task WHERE assignedbyId='"+emp.id+"';";
            }
            else
                tableCommand = "SELECT * FROM task WHERE assignedtoId='" + emp.id + "';";
            eriteDb(tableCommand);
        }

        private async void creat_Click(object sender, RoutedEventArgs e)
        {
            initializeData();
            //contentDialog1.Title = "Create Task";
            await contentDialog1.ShowAsync();
        }

        private void dialog_createClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Frame.Navigate(typeof(Display), emp);
        }
        private void eriteDb(string tableCommand)
        {
            tds.Clear();
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
                    }) ;
                }
                if(!(tds.Count>0))
                {
                    stark1.Visibility = Visibility.Collapsed;
                    star2.Visibility = Visibility.Visible;
                }
                var groups = from c in tds
                             group c by c.collective; 
                this.cvs.Source = groups;
                tasks.SelectedIndex = -1;
            }
        }

        private async void tasks_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            var clickedItem = e.ClickedItem;
            click = (td)clickedItem;
            data.emp = emp;
            data.click1 = click;
            Frame.Navigate(typeof(TaskDetailedView), data);

        }


/// <summary>
//           C# CODE FOR CONTENT DIALOG FOR CREATE A NEW TASK                                
/// </summary>

        private async void initializeData()
        {
            priority.Items.Clear();
            collective.Items.Clear();            
            priority.Items.Add("None");
            priority.Items.Add("Low");
            priority.Items.Add("Medium");
            priority.Items.Add("High");
            collective.Items.Add("sample");
            collective.Items.Add("Login");
            collective.Items.Add("Other");
            taskName.Text = "";
            taskDetails.Text = "";
            priority.SelectedIndex = 0;
            Assignto.SelectedIndex = -1;
            collective.SelectedIndex = 2;
            await teamsFromDatabase();
        }

        private async Task teamsFromDatabase()
        {
            Assignto.Items.Clear();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "SELECT * FROM emp WHERE team='" + emp.team + "' ;";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                while (reader.Read())
                {
                    Assignto.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                }

            }
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            string name = taskName.Text;
            string details = taskDetails.Text;
            string prior = priority.SelectedItem.ToString();
            string asign = Assignto.SelectedItem.ToString();
            string coll = collective.SelectedItem.ToString();

            if (name == "" || details == "" || asign == "" || coll == "")
                return;
            else
            {
                await writeinDb(name, details, prior, asign, coll);
                taskName.Text = "";
                taskDetails.Text = "";
                priority.SelectedIndex = 0;
                Assignto.SelectedIndex = -1;
                collective.SelectedIndex = 2;
                contentDialog1.Hide();
                data.emp = emp;
                data.click1 = click;
                Frame.Navigate(typeof(TaskDetailedView),data );
            }
        }

        private async Task writeinDb(string name, string details, string prior, string asign, string coll)
        {
            string n = await findIddb();
            await WriteInDB(n, name, details, prior, asign, coll);
        }
        private async Task WriteInDB(string n, string name, string details, string prior, string asign, string coll)
        {
            string[] assigned = asign.Split(" ");
            string status = "Open";
            var dt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
            click = new td { id = n, Assign_by = emp.name, Assign_by_id = emp.id, Assign_to = assigned[0], Assign_to_id = assigned[1], collective = coll, details = details, name = name, 
                updated = DateTime.Now, createdDate = DateTime.Now, team = emp.team, priority = prior, status = status };
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "INSERT INTO task(taskid,taskname,taskdetails,updated,createdDate,assignedby,assignedbyId,team,priority,status,assignedto,assignedtoId,collective)" +
                    "VALUES('" + n + "','" + name + "','" + details + "','" + dt + "','" + dt + "','" + emp.name + "','" + emp.id + "','" + emp.team + "','" + prior + "','" + status + "','" + assigned[0] + "','" + assigned[1] + "','" + coll + "');";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                
            }
        }

        private async Task<string> findIddb()
        {

            for (int i = 1; i < 1000; i++)
            {
                string s = "T-" + i.ToString();
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    string tableCommand = "SELECT * FROM task WHERE taskid='" + s + "' ;";
                    SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                    SqliteDataReader reader = createTable.ExecuteReader();
                    if (!reader.Read())
                    {
                        return s;
                    }

                }
            }
            return "No available taskId";
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            contentDialog1.Hide();
        }
    }
}
























/*

                            into g
                             orderby g.Key
                             select new { GroupName = g.Key, Items = g };
                group.Clear();
                foreach (var g in groups)
                {
                    GroupInfosList info = new GroupInfosList();
                    info.Key = g.GroupName + " (" + g.Items.Count() + ")";

                    foreach (var item in g.Items)
                    {
                        info.Add(item);
                    }

                    group.Add(info);
                }



*/