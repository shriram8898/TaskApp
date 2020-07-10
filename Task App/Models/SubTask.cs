using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Models
{
    public class SubTask:INotifyPropertyChanged
    {
        public string _id { get; set; }
        private string _name { get; set; }
        private string _details { get; set; }
        private string _priority { get; set; }
        private string _status { get; set; }
        private string _Assign_to { get; set; }
        
        private string _Assign_to_id { get; set; }
        private string _Assign_by { get; set; }
        private string _Assign_by_id { get; set; }
        private DateTime _createdDate { get; set; }
        private DateTime _completedDate { get; set; }
        private DateTime _updated { get; set; }
        private string _collective { get; set; }
        public string name
        {
            get { return _name; }

            set
            {
                _name = value;
                NotifyPropertyChanged("name");
            }
        }
        public string id
        {
            get { return _id; }

            set
            {
                _id = value;
                NotifyPropertyChanged("id");
            }
        }
        
        public string details
        {
            get { return _details; }

            set
            {
                _details = value;
                NotifyPropertyChanged("details");
            }
        }
        public string priority
        {
            get { return _priority; }

            set
            {
                _priority = value;
                NotifyPropertyChanged("priority");
            }
        }
        public string status
        {
            get { return _status; }

            set
            {
                _status = value;
                NotifyPropertyChanged("status");
            }
        }
        public string Assign_to
        {
            get { return _Assign_to; }

            set
            {
                _Assign_to = value;
                NotifyPropertyChanged("Assign_to");
            }
        }
        public string Assign_to_id
        {
            get { return _Assign_to_id; }

            set
            {
                _Assign_to_id = value;
                NotifyPropertyChanged("Assign_to_id");
            }
        }
        public string Assign_by_id
        {
            get { return _Assign_by_id; }

            set
            {
                _Assign_by_id = value;
                NotifyPropertyChanged("Assign_by_id");
            }
        }
        public string Assign_by
        {
            get { return _Assign_by; }

            set
            {
                _Assign_by = value;
                NotifyPropertyChanged("Assign_by");
            }
        }
        public string collective
        {
            get { return _collective; }

            set
            {
                _collective = value;
                NotifyPropertyChanged("collective");
            }
        }
        public DateTime createdDate
        {
            get { return _createdDate; }

            set
            {
                _createdDate = value;
                NotifyPropertyChanged("createdDate");
            }
        }
        public DateTime completedDate
        {
            get { return _completedDate; }

            set
            {
                _completedDate = value;
                NotifyPropertyChanged("completedDate");
            }
        }
        public DateTime updated
        {
            get { return _updated; }

            set
            {
                _updated = value;
                NotifyPropertyChanged("updated");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
