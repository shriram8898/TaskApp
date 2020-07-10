using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Models
{
    public class PassData
    {
        public Employee emp { get; set; }
        public TaskDetails click1 { get; set; }
        public ObservableCollection<TaskDetails> tds = new ObservableCollection<TaskDetails>();

    }
}
