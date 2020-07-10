using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Task_App.Models;
using System.Collections.ObjectModel;

namespace Task_App.Models
{
    public class DataBase
    {
        /// <summary>
        /// 
        /// Obtaining Details of the loged in user
        /// 
        /// </summary>
        public async static Task<Employee> VerifyDatabase(string tableCommand)
        {
            Employee emp = new Employee();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader =await createTable.ExecuteReaderAsync();
                if (reader.Read())
                {
                    emp = new Employee { id = reader.GetString(0), name = reader.GetString(1), designation = reader.GetString(3), 
                        username = reader.GetString(4), password = reader.GetString(5), role = reader.GetString(2) };
                    return emp;
                }
            }
            return emp;
        }


        public static async Task<ObservableCollection<TaskDetails>> ReadDataDb(string tableCommand)
        {
            ObservableCollection<TaskDetails> tds = new ObservableCollection<TaskDetails>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                while (reader.Read())
                {
                    tds.Add(new TaskDetails
                    {
                        id = reader.GetString(0),name = reader.GetString(1),details = reader.GetString(2),
                        Assign_to = reader.GetString(3),Assign_by = reader.GetString(4),Assign_to_id = reader.GetString(5),
                        Assign_by_id = reader.GetString(6),createdDate = reader.GetDateTime(9),subtaskid=reader.GetString(7),
                        priority = reader.GetString(11),status = reader.GetString(12),collective = reader.GetString(13),
                        updated = reader.GetDateTime(8)
                    });
                }                
            }
            return tds;
        }

        public async static Task<ObservableCollection<members>> LoadFromMembers(string tableCommand)
        {
            ObservableCollection<members> mem = new ObservableCollection<members>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                while (reader.Read())
                {
                    mem.Add(new members { name = reader.GetString(0), empid = reader.GetString(1), empname = reader.GetString(2),role = reader.GetString(3),designation = reader.GetString(4) });
                }
            }
            return mem;
        }

        public static async Task<ObservableCollection<string>> teamsFromDatabase(string tableCommand)
        {
            ObservableCollection<string> assign = new ObservableCollection<string>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                while (reader.Read())
                {
                    assign.Add(reader.GetString(2) + " " + reader.GetString(1));
                }
            }
            return assign;
        }
        public static async Task<bool> ExecuteCommand(string tableCommand)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader= createTable.ExecuteReader();
                if (reader.Read())
                    return true;
            }
            return false;
        }
        public static async Task<string> findIddb(string id)
        {
            for (int i = 1; i < 1000; i++)
            {
                string tableCommand;
                string s = id + i.ToString();
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    if(id=="T-")
                        tableCommand = "SELECT * FROM task WHERE taskid='" + s + "' ;";
                    else
                        tableCommand = "SELECT * FROM subtask WHERE subtaskid='" + s + "' ;";
                    SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                    SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                    if (!reader.Read())
                    {
                        return s;
                    }

                }
            }
            return "No available taskId";
        }

        public async static Task<string> EmpDetails(string tableCommand)
        {
            string value="";
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                if (reader.Read())
                {                    
                    value = reader.GetString(0)+" "+ reader.GetString(1);
                }
                    
            }
            return value;
        }

        public static async Task<string> FileReader(string tableCommand)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                string Value;
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                if (reader.Read())
                {
                    Value = reader.GetString(2);
                    return Value;
                }
            }
            return null;
        }

        public static async Task<ObservableCollection<comments>> LoadCommentsFromDatabase(string tableCommand)
        {
            ObservableCollection<comments> comments = new ObservableCollection<comments>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                while (reader.Read())
                {
                    string[] value = reader.GetString(1).Split(':');
                    comments.Add(new Models.comments { name = value[0], message = value[1], empid = reader.GetString(2), dt = reader.GetDateTime(3) });
                }
                    
            }
            return comments;
        }

        public static async Task<ObservableCollection<string>> LoadTeams(string tableCommand)
        {
            ObservableCollection<string> teams = new ObservableCollection<string>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                while (reader.Read())
                    teams.Add(reader.GetString(0));
            }
            return teams;
        }

        public static async Task<ObservableCollection<string>> LoadTeams2(string tableCommand)
        {
            ObservableCollection<string> teams = new ObservableCollection<string>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                while (reader.Read())
                    teams.Add(reader.GetString(2));
            }
            return teams;
        }

        public static async Task<ObservableCollection<Team>> LoadTeams1(string tableCommand)
        {
            ObservableCollection<Team> teams = new ObservableCollection<Team>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                while (reader.Read())
                    teams.Add(new Team { name=reader.GetString(0),type=reader.GetString(1),manager=reader.GetString(3)});
            }
            return teams;
        }

        public async static Task<bool> verify(string tableCommand)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                if (reader.Read())
                    return true;
            }
            return false;

        }


        public static async Task<ObservableCollection<Employee>> EmployeeDetails(string tableCommand)
        {
            ObservableCollection<Employee> assign = new ObservableCollection<Employee>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Task-App.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteDataReader reader = await createTable.ExecuteReaderAsync();
                while (reader.Read())
                {
                    assign.Add(new Employee { id=reader.GetString(0),name=reader.GetString(1),role=reader.GetString(2),designation=reader.GetString(3),username=reader.GetString(4)});
                }
            }
            return assign;
        }

    }
   
}
