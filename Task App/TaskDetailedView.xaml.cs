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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using System.Text;
using Windows.Storage;
using Windows.UI.ViewManagement.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskDetailedView : Page
    {
        public Employee emp;
        public TaskDetails click;
        public PassData data;
        public ObservableCollection<TaskDetails> tds;
        public ObservableCollection<TaskDetails> subtasks = new ObservableCollection<TaskDetails>();
        public ObservableCollection<comments> com1 = new ObservableCollection<comments>(new List<comments>(100));
        public TaskDetailedView()
        {
            this.InitializeComponent();
            PassData data = new PassData();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            data = e.Parameter as PassData;
            emp = data.emp;
            click = data.click1;
            tds = data.tds;
            tprior.Items.Clear();
            tstatus.Items.Clear();
            trelate.Items.Clear();
            tprior.Items.Add("None");
            tprior.Items.Add("Low");
            tprior.Items.Add("Medium");
            tprior.Items.Add("High");
            trelate.Items.Add("sample");
            trelate.Items.Add("Login");
            trelate.Items.Add("Other");
            tstatus.Items.Add("Open");
            tstatus.Items.Add("Close");
            initiaze();
        }
        private async void initiaze()
        {
            LoadSubTasks();
            com1.Clear();
            commentsSection.ItemsSource = null;
            this.DataContext = click;
            tprior.SelectedItem = click.priority;
            trelate.SelectedItem = click.collective;
            tstatus.SelectedItem = click.status;
            var groups = from c in tds
                         group c by c.collective;
            cvs.Source = groups;
            list.SelectedIndex = -1;
            files.Text = "";            
            if (!(click.Assign_by_id == emp.id || emp.designation == "manager"||emp.designation=="team lead"))
            {
                delete.Visibility = Visibility.Collapsed;
                edit.Visibility = Visibility.Collapsed;
                trelate.IsEditable = false;
                tstatus.IsEnabled = false;
                tprior.IsEnabled = false;
                trelate.IsEnabled = false;
            }
            if (click.Assign_to_id == emp.id)
            {
                complete.Visibility = Visibility.Visible;
            }
            tid.Text = click.id;
            tby.Text = click.Assign_by + "(" + click.Assign_by_id + ")";
            tto.Text = click.Assign_to + "(" + click.Assign_to_id + ")";
            tup.Text = click.updated.ToString();
            tcreated.Text = click.createdDate.ToString();
            string tableCommand = "SELECT * FROM comment WHERE taskid='" + click.id + "' ;";
            com1= await DataBase.LoadCommentsFromDatabase(tableCommand);
            commentsSection.ItemsSource = com1;
            tableCommand = "SELECT * FROM files WHERE taskid='" + click.id + "';";
            string value = await DataBase.FileReader(tableCommand);
            if (value!=null)
            {
                files.Text = value;
                files.Visibility = Visibility.Visible;
            }
        }
        private async void LoadSubTasks()
        {            
            string tableCommand = "SELECT * FROM subtask WHERE taskid='" + click.id + "';";
            subtasks.Clear();
            subtasks = await DataBase.ReadDataDb(tableCommand);
            this.cvs1.Source = null;
            var groups = from c in subtasks
                         group c by c.collective;
            this.cvs1.Source = groups;
        }
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TaskList), emp);
        }
        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            string tableCommand = "DELETE FROM task WHERE taskid='" + click.id + "';";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            tableCommand = "DELETE FROM files WHERE taskid='" + click.id + "';";
            result = await DataBase.ExecuteCommand(tableCommand);
            tableCommand = "DELETE FROM subtask WHERE taskid='" + click.id + "';";
            result = await DataBase.ExecuteCommand(tableCommand);
            this.Frame.Navigate(typeof(TaskList), emp);

        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            string mess = emp.name + ":" + combox.Text;
            string tableCommand = "INSERT INTO comment(taskid,comments,empid,date)" +
                    "VALUES('" + click.id + "','" + mess + "','" + emp.id + "','" + dt1 + "');";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            com1.Add(new comments { name = emp.name, message = combox.Text ,dt=dt1,empid=emp.id});
            commentsSection.ItemsSource = null;
            commentsSection.ItemsSource = com1;
            combox.Text = "";
        }
        private async void complete_Click(object sender, RoutedEventArgs e)
        {
            complete.Visibility = Visibility.Visible;
            string tableCommand = "UPDATE task SET status='Close' WHERE taskid='" + click.id + "'";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            if (!result)
            {

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
                output.Append("File Name   :" + file.Name + "\n");
                output.Append("File Created:" + file.DateCreated);
                files.Text = output.ToString();
                files.Visibility = Visibility.Visible;
                string tableCommand = "INSERT INTO files(taskid,name)" +
                    "VALUES('" + click.id + "','" + output.ToString() + "');";
                bool result = await DataBase.ExecuteCommand(tableCommand);
                if (!result)
                {

                }
            }

        }
        private async void edit_Click(object sender, RoutedEventArgs e)
        {
            initializeData();
            await contentDialog2.ShowAsync();
        }
        private void dialog_createClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Frame.Navigate(typeof(TaskDetailedView), data);
        }

        private void list_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem;
            var click1 = (TaskDetails)clickedItem;
            click = click1;
            initiaze();
        }

        private async void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tprior.SelectedIndex == -1)
                return;
            string prior = tprior.SelectedItem.ToString();
            string tableCommand = "UPDATE task SET priority='" + prior + "' WHERE taskid='" + click.id + "'";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            if (!result)
            {

            }
        }
        private async void combo_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            if (trelate.SelectedIndex == -1 )
                return;
            string coll = trelate.SelectedItem.ToString();
            string tableCommand = "UPDATE task SET collective='" + coll + "' WHERE taskid='" + click.id + "'";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            if (!result)
            {

            }
        }
        private async void combo_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            if ( tstatus.SelectedIndex == -1 )
                return;
            string status = tstatus.SelectedItem.ToString();
            string tableCommand = "UPDATE task SET status='" + status + "' WHERE taskid='" + click.id + "'";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            if (!result)
            {

            }
        }

        // C# CODE FOR EDIT CONTENT DIALOG
        private async void initializeData()
        {
            priority1.Items.Clear();
            collective1.Items.Clear();
            status.Items.Clear();
            priority1.Items.Add("None");
            priority1.Items.Add("Low");
            priority1.Items.Add("Medium");
            priority1.Items.Add("High");
            collective1.Items.Add("sample");
            collective1.Items.Add("Login");
            collective1.Items.Add("Other");
            status.Items.Add("Open");
            status.Items.Add("Close");

        }
        private async void save_Click(object sender, RoutedEventArgs e)
        {
            string name = taskName1.Text;
            string details = taskDetails1.Text;
            string prior = priority1.SelectedItem.ToString();

            string coll = collective1.SelectedItem.ToString();
            string status = "Open";
            var dt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
            click.updated = DateTime.Now; click.name = name; click.details = details; click.priority = prior; click.status = status; click.collective = coll;
            string tableCommand = "UPDATE task SET updated='" + dt + "',taskname='" + name + "',taskdetails='" + details + "',priority='" + prior + "',status='" + status + "',collective='" + coll + "' WHERE taskid='" + click.id + "'";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            if (!result)
            {

            }
            contentDialog2.Hide();
            data.click1 = click;

        }

        private void edit1_Click(object sender, RoutedEventArgs e)
        {
            taskDetails1.Visibility = Visibility.Visible;
        }
        private void cancel1_Click(object sender, RoutedEventArgs e)
        {
            contentDialog2.Hide();
        }

        private async void subtask_Click(object sender, RoutedEventArgs e)
        {
            initializeData1();
            await contentDialog1.ShowAsync();
        }

        public ObservableCollection<string> temp = new ObservableCollection<string>();
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
                tableCommand = "SELECT * FROM members WHERE name='" + item + "';";
                assign = await DataBase.teamsFromDatabase(tableCommand);
                foreach (string vari in assign)
                    select1.Add(vari);
            }
            return select1;
        }
        private async void initializeData1()
        {
            priority.Items.Clear();
            priority.Items.Add("None");
            priority.Items.Add("Low");
            priority.Items.Add("Medium");
            priority.Items.Add("High");
            taskName.Text = "";
            taskDetails.Text = "";
            priority.SelectedIndex = 0;
            Assignto.SelectedIndex = -1;
            temp = await LoadTeamMembers();
            Assignto.ItemsSource = temp;
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            string name = taskName.Text;
            string details = taskDetails.Text;
            string prior = priority.SelectedItem.ToString();
            string asign = Assignto.SelectedItem.ToString();

            if (name == "" || details == "" || asign == "" )
                return;
            else
            {
                await writeinDb(name, details, prior, asign);
                taskName.Text = "";
                taskDetails.Text = "";
                priority.SelectedIndex = 0;
                Assignto.SelectedIndex = -1;
                contentDialog1.Hide();
                data.emp = emp;
                data.click1 = click;
                data.tds = tds;
                this.Frame.Navigate(typeof(TaskDetailedView), data);
            }
        }

        private async Task writeinDb(string name, string details, string prior, string asign)
        {
            string n = await DataBase.findIddb("ST-");
            string[] assigned = asign.Split(" ");
            string status = "Open";
            string coll = click.collective;
            var dt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
            string tableCommand = "INSERT INTO subtask(subtaskid,name,details,updated,created,assignby,assignbyid,priority,status,assignto,assigntoid,taskid,collective)" +
                    "VALUES('" + n + "','" + name + "','" + details + "','" + dt + "','" + dt + "','" + emp.name + "','" + emp.id + "','" + prior + "','" + status + "','" + assigned[0] + "','" + assigned[1] + "','" + click.id + "','"+click.collective+"');";
            bool result = await DataBase.ExecuteCommand(tableCommand);
            if (!result)
            {

            }
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
            
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            contentDialog1.Hide();
        }

        private void subtaskview_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem;
            data.click1 = (TaskDetails)clickedItem;
            this.Frame.Navigate(typeof(TaskDetailedView), data);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CoreInputView.GetForCurrentView().TryShow(CoreInputViewKind.Emoji);
        }
    }
}
