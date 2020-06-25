using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App
{
    public class PasData
    {
        public Employee emp { get; set; }
        public td click1 { get; set; }
        public ObservableCollection<td> tds = new ObservableCollection<td>();
    }
}
