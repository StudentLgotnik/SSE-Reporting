using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.ViewModel
{
    class InfoViewModel : INotifyPropertyChanged
    {
        private DBContext context;
        private Employee selectedEmployee;

        public String Employee
        {
            get { return selectedEmployee.ToString(); }
            set
            {
                selectedEmployee = new Employee();
                OnPropertyChanged("EmployeeToString");
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        public InfoViewModel(DBContext context, Employee empl)
        {
            this.context = context;
            selectedEmployee = empl;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
