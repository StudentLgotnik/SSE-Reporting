using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Services
{
	class SalaryManager
	{
        //Strategy pattern realization
		PayStrategy payStrategy;

		public SalaryManager(PayStrategy payStrategy)
		{
			this.payStrategy = payStrategy;
		}

        public double getSalary(Employee employee)
        {
            return payStrategy.calcSalary(employee);
        }

        public PayStrategy PayStrategy
        {
            get { return payStrategy; }
            set
            {
                payStrategy = value;
                OnPropertyChanged("EmployeeId");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
