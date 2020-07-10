using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Task_App.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TeamDetails : Page
    {
        public ObservableCollection<Team> team = new ObservableCollection<Team>();
        public ObservableCollection<members> mem = new ObservableCollection<members>();
        public Employee emp;
        public Team t;
        public TeamDetails()
        {
            this.InitializeComponent();
            //this.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/img.jpg")), Stretch = Stretch.UniformToFill };
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            emp = e.Parameter as Employee;
            if (!(emp.designation == "manager"))
                Add_Team.Visibility = Visibility.Collapsed;
            LoadListItems();
            t = team[0];
            LoadMembersItems();
            
        }

        private async void LoadMembersItems()
        {
            string tableCommand = "SELECT * FROM members WHERE name='" + t.name + "';";
            mem = await DataBase.LoadFromMembers(tableCommand);
            members.ItemsSource = null;
            members.ItemsSource = mem;
        }

        private async void LoadListItems()
        {
            
            string tableCommand = "SELECT * FROM teams;";
            ObservableCollection<Team> teams = await DataBase.LoadTeams1(tableCommand);
            foreach (var item in teams)
            {
                tableCommand = "SELECT * FROM members WHERE name='" + item.name + "' AND designation='team head';";
                var head = await DataBase.LoadTeams2(tableCommand);
                if(head.Count>0)
                item.head = head[0];
                tableCommand = "SELECT * FROM members WHERE name='" + item.name + "';";
                ObservableCollection<string> members = await DataBase.teamsFromDatabase(tableCommand);
                item.count = members.Count.ToString();
                team.Add(item);
            }
        }

        private async void Add_Team_Click(object sender, RoutedEventArgs e)
        {
            await Addteam.ShowAsync();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string tableCommand;
            string name = teamName.Text;
            string type = teamType.Text;
            if (name == "" || type == "")
                return;
            tableCommand="SELECT * FROM teams WHERE name='"+name+"';";
            bool result = await DataBase.verify(tableCommand);
            if(result==false)
            {
                tableCommand = "INSERT INTO teams(name,type,empid,empname)" +
               "VALUES('" + name + "','" + type + "','" + emp.id + "','" + emp.name + "');";
                result = await DataBase.ExecuteCommand(tableCommand);
                Addteam.Hide();
            }
           
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Addteam.Hide();
        }

        private void teams_ItemClick(object sender, ItemClickEventArgs e)
        {
            t =(Team)e.ClickedItem;
            LoadMembersItems();
        }

        private async void add1_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> temp = await LoadTeamMembers();
            ObservableCollection<string> temp2 = new ObservableCollection<string>();
            foreach(var item in mem)
            {
                temp2.Add(item.empname + " " + item.empid);
            }
            temp2 = new ObservableCollection<string>(temp.Except(temp2));
            ObservableCollection<EmployeeDisplay> temp1 = new ObservableCollection<EmployeeDisplay>();
            foreach(var items in temp2)
            {
                string[] item = items.Split(' ');
                string pic = "Assets/" + item[1] + ".jpg";
                temp1.Add(new EmployeeDisplay { name = item[0], Img = new BitmapImage(new Uri(this.BaseUri,pic)), empid=item[1]});
            }
            employee.ItemsSource = temp1;
            await add_member.ShowAsync();
        }
        private async Task<ObservableCollection<string>> LoadTeamMembers()
        {
            string tableCommand;
            ObservableCollection<Employee> assign;
            ObservableCollection<string> select1 = new ObservableCollection<string>();
            tableCommand = "SELECT * FROM emp;";
            assign = await DataBase.EmployeeDetails(tableCommand);
            foreach (var vari in assign)
                select1.Add(vari.name + " " + vari.id);
            return select1;
        }
        private async void add2_Click(object sender, RoutedEventArgs e)
        {
            var item = (EmployeeDisplay)employee.SelectedItem;
            var items = item.name + " " + item.empid;
            string[] name = items.Split(' ');
            string tableCommand = "SELECT role,designation FROM emp WHERE empid='" + name[1] + "';";
            string role1 = await DataBase.EmpDetails(tableCommand);
            string[] role = role1.Split(' ');
            tableCommand = "INSERT INTO members(name,empid,empname,role,designation)" +
                "VALUES('" + t.name + "','" + name[1] + "','" + name[0] + "','" + role[0] +" " +role[1] + "','" + role[2] + "');";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            mem.Add(new Models.members { name = t.name, empid = name[1], empname = name[0], role = role[0]+" "+role[1], designation = role[2] });
            add_member.Hide();
        }

        private void close1_Click(object sender, RoutedEventArgs e)
        {
            add_member.Hide();
        }
    }
}
