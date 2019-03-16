using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model
{
    public class Task : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private Activity activity;
        private int? project_id;

        public Task()
        {
            id = 0;
            name = null;
            project_id = null;
        }

        public Task(string name, Activity activity)
        {
            this.name = name;
            this.activity = activity;
            project_id = null;
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("TaskId");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("TaskName");
            }
        }

        public Activity Activity
        {
            get { return activity; }
            set
            {
                activity = value;
                OnPropertyChanged("TaskActivity");
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

        public static Builder getBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private Task task;
            public Builder()
            {
                task = new Task();
            }

            public Builder Id(int id)
            {
                task.Id = id;
                return this;
            }

            public Builder Name(string name)
            {
                task.Name = name;
                return this;
            }

            public Builder Activity(Activity activity)
            {
                task.Activity = activity;
                return this;
            }

            public Builder ProjectId(int id)
            {
                task.ProjectId = id;
                return this;
            }

            public Task Build()
            {
                return task;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}  {1}", Name, Activity);
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
            var task = obj as Task;
            return task != null &&
                   id == task.id &&
                   name == task.name &&
                   activity == task.activity;
        }

        public override int GetHashCode()
        {
            var hashCode = 29434786;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + activity.GetHashCode();
            return hashCode;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
