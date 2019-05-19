using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model.Employments;
using SSE_Reporting.Model.Positions.Levels;

namespace SSE_Reporting.Model.Positions
{
	public abstract class Position : INotifyPropertyChanged
	{

		private int id;
		private string name;
        //Bridge pattern aggregation
		private Level level;
		private int? levelId;

		protected Position()
		{
			id = 0;
			name = null;
			level = null;
		}

		protected Position(int id, string name, Level level)
		{
			this.id = id;
			this.name = name;
			this.level = level;
		}

		public int Id
		{
			get { return id; }
			set
			{
				id = value;
				OnPropertyChanged("PositionId");
			}
		}

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				OnPropertyChanged("PositionName");
			}
		}

		public Level Level
		{
			get { return level; }
			set
			{
				level = value;
				OnPropertyChanged("Level");
			}
		}

        public int? LevelId
        {
            get { return levelId; }
            set
            {
                levelId = value;
                OnPropertyChanged("LevelId");
            }
        }

        public double getSalary()
		{
			return level.getSalary();
		}

		public override string ToString()
		{
			return name;
		}

        //Abstract factory method
        public abstract Employment GetEmployment();

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
            var position = obj as Position;
            return position != null &&
                   name == position.name;
        }

        public event PropertyChangedEventHandler PropertyChanged;


	}
}
