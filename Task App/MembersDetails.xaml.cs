using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Task_App.Models;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MembersDetails : Page
    {
        public Employee emp;
        public ObservableCollection<Employee> emp1 = new ObservableCollection<Employee>();
        public MembersDetails()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            emp = e.Parameter as Employee;
            string tableCommand = "SELECT * FROM emp";
            emp1 = await DataBase.EmployeeDetails(tableCommand);
            if (!(emp.designation == "manager"))
                Addmember.Visibility = Visibility.Collapsed;
        }
        private async void InitializeData()
        {
            Role.Items.Clear();
            Position.Items.Clear();
            Role.Items.Add("developer");
            Role.Items.Add("quality analysis");
            Position.Items.Add("manager");
            Position.Items.Add("team leader");
            Position.Items.Add("member technical staff");
            string tableCommand = "SELECT * FROM teams WHERE empid='" + emp.id + "';";
            
            
        }
        private async void add1_Click(object sender, RoutedEventArgs e)
        {
            string name1 = empname.Text;
            string id1 = empid.Text;
            string role1 = Role.SelectedItem.ToString();
            string email1 = Email.Text;
            string position1 = Position.SelectedItem.ToString();
            if (id1 == "" || name1 == "" || email1 == "" || role1 == "" || position1 == "")
                return;
            string tableCommand = "INSERT INTO emp(empid,empname,designation,role,email,password)" +
                "VALUES('" + id1 + "','" + name1 + "','" + position1 + "','" + role1 + "','" + email1 + "','password');";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            emp1.Add(new Employee { id = id1, name = name1, username = email1, role = role1, designation = position1 });
            tableCommand = "INSERT INTO settings(empid,dark,alltask)" +
                "VALUES('" + id1 + "','no','no');";
            result = await DataBase.ExecuteCommand(tableCommand);
            Addmembers.Hide();
        }

        private void close1_Click(object sender, RoutedEventArgs e)
        {
            Addmembers.Hide();
        }

        private async void Addmembers_Click(object sender, RoutedEventArgs e)
        {
            InitializeData();
            await Addmembers.ShowAsync();
        }

        private void Members_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        

        
    }
}
