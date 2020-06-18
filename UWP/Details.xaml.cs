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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Details : Page
    {
        public Employee emp = new Employee();
        public Details(Employee em)
        {
            emp = em;
            this.InitializeComponent();
            initializeData();
        }

        private async void initializeData()
        {
            priority.Items.Add("None");
            priority.Items.Add("Low");
            priority.Items.Add("Medium");
            priority.Items.Add("High");
            collective.Items.Add("sample");
            collective.Items.Add("Login");
            collective.Items.Add("Other");
            await teamsFromDatabase();
        }

        private async Task teamsFromDatabase()
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "SELECT * FROM emp WHERE team='" + emp.team + "' ;";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = createTable.ExecuteReader();
                while(reader.Read())
                {
                    Assignto.Items.Add(reader.GetString(1)+" "+reader.GetString(0));
                }
                
            }
        }

        private async  void add_Click(object sender, RoutedEventArgs e)
        {
            string name = taskName.Text;
            string details = taskDetails.Text;
            string prior = priority.SelectedItem.ToString();
            string asign = Assignto.SelectedItem.ToString();
            string coll = collective.SelectedItem.ToString();

            if (name == "" || details == ""||asign==""||coll=="")
                return;
            else
            {
                await writeinDb(name, details, prior,asign,coll);
                taskName.Text = "";
                taskDetails.Text = "";
                priority.SelectedIndex = 0;
                Assignto.SelectedIndex = -1;
                collective.SelectedIndex = 2;
                
            }
        }

        private async Task writeinDb(string name, string details, string prior,string asign,string coll)
        {
            string n = await findIddb();
            await WriteInDB(n,name,details,prior,asign,coll);
        }
        private async Task WriteInDB(string n, string name, string details, string prior, string asign, string coll)
        {
            string[] assigned = asign.Split(" ");
            string status = "Open";
            var dt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "taskdb.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "INSERT INTO task(taskid,taskname,taskdetails,updated,createdDate,assignedby,assignedbyId,team,priority,status,assignedto,assignedtoId,collective)" +
                    "VALUES('" + n + "','" + name + "','"+details+ "','"+dt+ "','" + dt + "','" + emp.name+"','" + emp.id + "','"+emp.team+"','"+prior+"','"+status+"','"+assigned[0]+"','"+ assigned[1] + "','"+coll+"');";
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
    }
}
