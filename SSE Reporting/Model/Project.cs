using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model
{
    public class Project : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string company;
        private List<Task> tasks;
        private List<Employee> employees;

        public Project()
        {
            id = 0;
            name =
            company = null;
            tasks = new List<Task>();
            employees = new List<Employee>();
        }

        public Project(string name, string company)
        {
            this.name = name;
            this.company = company;
            tasks = new List<Task>();
            employees = new List<Employee>();
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ProjectId");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("ProjectName");
            }
        }

        public string Company
        {
            get { return company; }
            set
            {
                company = value;
                OnPropertyChanged("Company");
            }
        }

        public List<Task> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        public List<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        public void addTask(Task task)
        {
            Tasks.Add(task);
        }

        public void addEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public static Builder getBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private Project project;
            public Builder()
            {
                project = new Project();
            }

            public Builder Id(int id)
            {
                project.Id = id;
                return this;
            }
            public Builder Name(string name)
            {
                project.Name = name;
                return this;
            }

            public Builder Company(string company)
            {
                project.Company = company;
                return this;
            }

            public Builder Tasks(List<Task> tasks)
            {
                project.Tasks = tasks;
                return this;
            }

            public Builder Employees(List<Employee> employees)
            {
                project.Employees = employees;
                return this;
            }

            public Project Build()
            {
                return project;
            }
        }


        public override string ToString()
        {
            return String.Format("{0} - {1}", Company, Name);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }

        public override bool Equals(object obj)
        {
            var project = obj as Project;
            return project != null &&
                   id == project.id &&
                   name == project.name &&
                   company == project.company &&
                   EqualityComparer<List<Task>>.Default.Equals(tasks, project.tasks);
        }

        public override int GetHashCode()
        {
            var hashCode = -399616497;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(company);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Task>>.Default.GetHashCode(tasks);
            return hashCode;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
