using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App
{
    public class itemnotify : INotifyPropertyChanged
    {
        // Properties
        private string tname;
        private string tdetail;
        private string priority;
        private string status;
        private string coll;


        public string taskName
        {
            get { return tname; }

            set
            {
                tname = value;
                NotifyPropertyChanged("taskName");
            }
        }

        public string taskDetails
        {
            get { return tdetail; }

            set
            {
                tdetail = value;
                NotifyPropertyChanged("taskDetails");
            }
        }

        public string prior
        {
            get { return priority; }
            set
            {
                priority = value;
                NotifyPropertyChanged("prior");
            }
        }
        public string statu
        {
            get { return status; }
            set
            {
                status = value;
                NotifyPropertyChanged("statu");
            }
        }
        public string collective
        {
            get { return coll; }
            set
            {
                coll = value;
                NotifyPropertyChanged("collective");
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
