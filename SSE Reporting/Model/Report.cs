using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model
{
    public class Report : INotifyPropertyChanged
    {
        private int id;
        private Project project;
        private Task task;
        private int? employee_id;
        private Activity activity;
        private TimeSpan startHours;
        private TimeSpan endHours;
        private DateTime date;
        private string comment;

        public Report()
        {
            id = 0;
            project = null;
            task = null;
            startHours =
            endHours = new TimeSpan();
            date = new DateTime();
        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ReportId");
            }
        }
        public Project Project
        {
            get { return project; }
            set
            {
                project = value;
                OnPropertyChanged("Project");
            }
        }

        public Task Task
        {
            get { return task; }
            set
            {
                task = value;
                OnPropertyChanged("Task");
            }
        }

        public int? EmployeeId
        {
            get { return employee_id; }
            set
            {
                employee_id = value;
                OnPropertyChanged("EmployeeId");
            }
        }

        public Activity Activity
        {
            get { return activity; }
            set
            {
                activity = value;
                OnPropertyChanged("ProjectActivity");
            }
        }

        public TimeSpan StartHours
        {
            get { return startHours; }
            set
            {
                startHours = value;
                OnPropertyChanged("StartHours");
            }
        }

        public TimeSpan EndHours
        {
            get { return endHours; }
            set
            {
                endHours = value;
                OnPropertyChanged("EndHours");
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public string Commect
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Date");
            }
        }

        public static Builder getBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private Report report;
            public Builder()
            {
                report = new Report();
            }

            public Builder Id(int id)
            {
                report.Id = id;
                return this;
            }
            public Builder Project(Project project)
            {
                report.Project = project;
                return this;
            }

            public Builder Task(Task task)
            {
                report.Task = task;
                return this;
            }

            public Builder EmployeeId(int id)
            {
                report.EmployeeId = id;
                return this;
            }

            public Builder Activity(Activity activiti)
            {
                report.Activity = activiti;
                return this;
            }

            public Builder StartHours(TimeSpan timeSpan)
            {
                report.StartHours = timeSpan;
                return this;
            }

            public Builder EndHours(TimeSpan timeSpan)
            {
                report.EndHours = timeSpan;
                return this;
            }

            public Builder Date(DateTime date)
            {
                report.Date = date;
                return this;
            }

            public Builder Commect(string comment)
            {
                report.Commect = comment;
                return this;
            }

            public Report Build()
            {
                return report;
            }
        }

        public override string ToString()
        {
            IRepository<Employee> ir = EmployeeImpl.getInstance(new DBContext());
            Employee empl = ir.get((int)EmployeeId);
            return String.Format("{0} ({1})   {2}   {3}[{4} - {5}]", Project, empl, Task, Date.ToString("dd/MM/yyyy"), StartHours, EndHours);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
