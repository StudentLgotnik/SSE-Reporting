using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model.Positions.Levels
{
	public abstract class Level : INotifyPropertyChanged
	{

		private int id;
		private string name;
		private double salary;

		protected Level()
		{
			id = 0;
			salary = 0.0;
			name = null;
		}

		protected Level(int id, string name, double salary)
		{
			this.id = id;
			this.name = name;
			this.salary = salary;
		}

		public string getLevel()
		{
			return name;
		}

		public double getSalary()
		{
			return salary;
		}

		public int Id
		{
			get { return id; }
			set
			{
				id = value;
				OnPropertyChanged("LevelId");
			}
		}

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				OnPropertyChanged("LevelName");
			}
		}

		public double Salary
		{
			get { return salary; }
			set
			{
				salary = value;
				OnPropertyChanged("Salary");
			}
		}

		public override string ToString()
		{
			return name;
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
