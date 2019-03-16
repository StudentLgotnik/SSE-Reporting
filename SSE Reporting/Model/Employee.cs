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
    public class Employee : INotifyPropertyChanged
    {
        private int id;
        private string login;
        private string password;
        private double timeOff;
        private double sickness;
        private int? project_id;
        private Role role;

        public Employee()
        {
            id = 0;
            login =
            password = null;
            timeOff =
            sickness = 0;
            project_id = null;
            role = Role.User;
        }

        public Employee(string login, string password)
        {
            id = 0;
            this.login = login;
            this.password = password;
            timeOff = 0;
            project_id = null;
            sickness = 20;
            role = Role.User;
        }


        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("EmployeeId");
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public double TimeOff
        {
            get { return timeOff; }
            set
            {
                timeOff = value;
                OnPropertyChanged("TimeOff");
            }
        }

        public double Sickness
        {
            get { return sickness; }
            set
            {
                sickness = value;
                OnPropertyChanged("Sickness");
            }
        }

        public int? ProjectId
        {
            get { return project_id; }
            set
            {
                project_id = value;
                OnPropertyChanged("Projects");
            }
        }

        public Role Role
        {
            get { return role; }
            set
            {
                role = value;
                OnPropertyChanged("Role");
            }
        }

        public static Builder getBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private Employee employee;
            public Builder()
            {
                employee = new Employee();
            }

            public Builder Id(int id)
            {
                employee.Id = id;
                return this;
            }
            public Builder Login(string login)
            {
                employee.Login = login;
                return this;
            }

            public Builder Password(string password)
            {
                employee.Password = password;
                return this;
            }

            public Builder TimeOff(double timeOff)
            {
                employee.TimeOff = timeOff;
                return this;
            }

            public Builder Sickness(double sickness)
            {
                employee.Sickness = sickness;
                return this;
            }

            public Builder ProjectId(int id)
            {
                employee.ProjectId = id;
                return this;
            }

            public Builder Role(Role role)
            {
                employee.Role = role;
                return this;
            }

            public Employee Build()
            {
                return employee;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}", Login);
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
            var employee = obj as Employee;
            return employee != null &&
                   id == employee.id &&
                   login == employee.login &&
                   password == employee.password &&
                   timeOff == employee.timeOff &&
                   sickness == employee.sickness &&
                   project_id == employee.project_id &&
                   role == employee.role;
        }

        public override int GetHashCode()
        {
            var hashCode = 1713803354;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(login);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            hashCode = hashCode * -1521134295 + timeOff.GetHashCode();
            hashCode = hashCode * -1521134295 + sickness.GetHashCode();
            hashCode = hashCode * -1521134295 + project_id.GetHashCode();
            hashCode = hashCode * -1521134295 + role.GetHashCode();
            return hashCode;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
