using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using SSE_Reporting.Model;
using SSE_Reporting.Model.Positions;
using SSE_Reporting.Model.Positions.Levels;
using SSE_Reporting.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private PayStrategy payStrategy;
        private double salary;
        private ObservableCollection<PayStrategy> payStrategys = new ObservableCollection<PayStrategy>();

        public String Employee
        {
            get { return selectedEmployee.ToString(); }
            set
            {
                selectedEmployee = new Employee();
                OnPropertyChanged("EmployeeToString");
            }
        }

        public double Salary
        {
            get {
                return salary; }
            set
            {

                salary = value;
                OnPropertyChanged("Salary");
            }
        }

        public ObservableCollection<PayStrategy> PayStrategys
        {
            get { return payStrategys; }
            set
            {
                payStrategys = value;
                OnPropertyChanged("PayStrategys");
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

        public PayStrategy PayStrategy
        {
            get {
                return payStrategy; }
            set
            {
                payStrategy = value;
                if (SelectedEmployee.Position != null)
                {
                    Salary = Math.Round(new SalaryManager(PayStrategy).getSalary(selectedEmployee), 2);
                }
                OnPropertyChanged("PayStrategy");
            }
        }

        public InfoViewModel(DBContext context, Employee empl)
        {
            selectedEmployee = empl;
            this.context = context; 
            IRepository<Position> positionRepo = PositionImpl.getInstance(context);
            IRepository<Level> levelRepo = LevelImpl.getInstance(context);

            PayStrategy = new MonthStrategy();
            if (empl.PositionId != null)
            {
                empl.Position = positionRepo.get((int)empl.PositionId);
                if (empl.Position.LevelId != null)
                {
                    empl.Position.Level = levelRepo.get((int)empl.Position.LevelId);
                    Salary = Math.Round(new SalaryManager(PayStrategy).getSalary(empl),2);
                }
            }
           
            payStrategys = new ObservableCollection<PayStrategy>(new List<PayStrategy>(new PayStrategy[] { PayStrategy, new IncomeMaonthStrategy(), new QuarterStrategy(), new YearStrategy() }));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
