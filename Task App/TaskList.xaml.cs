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
using Task_App.Models;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskList : Page
    {
        public ObservableCollection<TaskDetails> tds = new ObservableCollection<TaskDetails>();
        public PassData data;
        public Employee emp;
        public TaskDetails click;
        public ObservableCollection<string> temp = new ObservableCollection<string>();
        public TaskList()
        {
            this.InitializeComponent();
            data = new PassData();
            select.Items.Add("All");
            select.Items.Add("Assigned by me");
            select.Items.Add("Assigned to");
            
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {          
            emp = e.Parameter as Employee;
            temp=await LoadTeamMembers();
            select1.ItemsSource = temp;
        }

        private async void creat_Click(object sender, RoutedEventArgs e)
        {
            initializeData();
            await contentDialog1.ShowAsync();
        }
        private async void select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            star2.Visibility = Visibility.Collapsed;
            stark1.Visibility = Visibility.Visible;                     
            string tableCommand = "";
            if (select.SelectedItem.ToString() == "All")
            {
                employees.Visibility = Visibility.Collapsed;
                tableCommand = "SELECT * FROM task;";
            }
            else if (select.SelectedItem.ToString() == "Assigned by me")
            {
                employees.Visibility = Visibility.Collapsed;
                tableCommand = "SELECT * FROM task WHERE assignedbyId='" + emp.id + "';";
            }
            else if (select.SelectedItem.ToString() == "Assigned to" && employees.Visibility != Visibility.Visible)
            {
                employees.Visibility = Visibility.Visible;
                select1.SelectedIndex= - 1;
                return;
            }
            else if(select1.SelectedIndex!=-1)
            { 
                string[] value = select1.SelectedItem.ToString().Split(' ');
                string val = value[1];
                tableCommand = "SELECT * FROM task WHERE assignedtoId='" + val + "';";
            }
            else
                return;
                
            tds.Clear();
            tds= await DataBase.ReadDataDb(tableCommand);
            if (!(tds.Count > 0))
            {
                stark1.Visibility = Visibility.Collapsed;
                star2.Visibility = Visibility.Visible;
            }
            var groups = from c in tds
                         group c by c.collective;
            this.cvs.Source = groups;
            tasks.SelectedIndex = -1;
        }
        private void dialog_createClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Frame.Navigate(typeof(DashBoard), emp);
        }      
        private async void tasks_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem;
            click = (TaskDetails)clickedItem;
            data.emp = emp;
            data.click1 = click;
            data.tds = tds;
            this.Frame.Navigate(typeof(TaskDetailedView), data);

        }
        private async Task<ObservableCollection<string>> LoadTeamMembers()
        {
            string tableCommand;
            ObservableCollection<string> assign;
            ObservableCollection<string> select1 = new ObservableCollection<string>();
            if (emp.designation == "manager")
            {
                tableCommand = "SELECT * FROM teams WHERE empid='" + emp.id + "';";
            }
            else
            {
                tableCommand = "SELECT * FROM members WHERE empid='" + emp.id + "';";                
            }
            ObservableCollection<string> teams = await DataBase.LoadTeams(tableCommand);
            foreach (var item in teams)
            {
                tableCommand = "SELECT * FROM members WHERE name='"+item+"';";
                assign = await DataBase.teamsFromDatabase(tableCommand);
                foreach (string vari in assign)
                    select1.Add(vari);
            }
            return select1;
        }


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
            temp = await LoadTeamMembers();
            ObservableCollection<EmployeeDisplay> temp1 = new ObservableCollection<EmployeeDisplay>();
            foreach (var items in temp)
            {
                string[] item = items.Split(' ');
                string pic = "Assets/" + item[1] + ".jpg";
                temp1.Add(new EmployeeDisplay { name = item[0], Img = new BitmapImage(new Uri(this.BaseUri, pic)), empid = item[1] });
            }
            Assignto.ItemsSource = temp1;
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            string name = taskName.Text;
            string details = taskDetails.Text;
            string prior = priority.SelectedItem.ToString();
            var emplo = (EmployeeDisplay)Assignto.SelectedItem;
            string asign = emplo.name + " " + emplo.empid;
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
                data.tds = tds;
                this.Frame.Navigate(typeof(TaskDetailedView), data);
            }
        }

        private async Task writeinDb(string name, string details, string prior, string asign, string coll)
        {
            string n = await DataBase.findIddb("T-");
            string[] assigned = asign.Split(" ");
            string status = "Open";
            var dt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
            click = new TaskDetails
            {
                id = n,
                Assign_by = emp.name,
                Assign_by_id = emp.id,
                Assign_to = assigned[0],
                Assign_to_id = assigned[1],
                collective = coll,
                details = details,
                name = name,
                updated = DateTime.Now,
                createdDate = DateTime.Now,
                priority = prior,
                status = status
            };
            tds.Add(click);
            string tableCommand = "INSERT INTO task(taskid,taskname,taskdetails,updated,createdDate,assignedby,assignedbyId,priority,status,assignedto,assignedtoId,collective,team)" +
                    "VALUES('" + n + "','" + name + "','" + details + "','" + dt + "','" + dt + "','" + emp.name + "','" + emp.id + "','" + prior + "','" + status + "','" + assigned[0] + "','" + assigned[1] + "','" + coll + "','Assets/"+emp.id+".jpg');";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            if (!result)
            {

            }
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            contentDialog1.Hide();
        }
    }
}
