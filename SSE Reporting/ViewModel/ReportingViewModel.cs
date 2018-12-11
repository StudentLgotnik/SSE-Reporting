using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using SSE_Reporting.Model;
using SSE_Reporting.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace SSE_Reporting.ViewModel
{
    class ReportingViewModel : INotifyPropertyChanged, IDisposable
    {
        private DBContext dbContext;

        IRepository<Employee> employeeRepo;
        IRepository<Project> projectRepo;
        IRepository<Report> reportRepo;
        IRepository<Task> taskRepo;

        private Employee currentEmpl;
        private DateTime selectedDay;
        private string selectedMonth;
        private Report selectedReport;

        private string mondayHeader;
        private string tuesdayHeader;
        private string wednesdayHeader;
        private string thursdayHeader;
        private string fridayHeader;
        private string saturdayHeader;
        private string sundayHeader;

        private int selectedIndex;
        private string reportCommand;

        private RelayCommand addReport;
        private RelayCommand deleteReport;
        private RelayCommand nextWeek;
        private RelayCommand previousWeek;
        private RelayCommand info;
        private RelayCommand adminPanel;

        private ObservableCollection<Report> mondayReports = new ObservableCollection<Report>();
        private ObservableCollection<Report> tuesdayReports = new ObservableCollection<Report>();
        private ObservableCollection<Report> wednwsdayReports = new ObservableCollection<Report>();
        private ObservableCollection<Report> thursdayReports = new ObservableCollection<Report>();
        private ObservableCollection<Report> fridayReports = new ObservableCollection<Report>();
        private ObservableCollection<Report> saturdayReports = new ObservableCollection<Report>();
        private ObservableCollection<Report> sundayReports = new ObservableCollection<Report>();

        private double mondayProgress;
        private double tuesdayProgress;
        private double wednesdayProgress;
        private double thursdayProgress;
        private double fridayProgress;
        private double saturdayProgress;
        private double sundayProgress;

        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        //private ObservableCollection<Activity> activityEnum = new ObservableCollection<Activity>();

        public Report SelectedReport
        {
            get { return selectedReport; }
            set
            {
                selectedReport = value;
                OnPropertyChanged("SelectedReport");
            }
        }

        public string ReportCommand
        {
            get
            {
                return reportCommand;
            }
            set
            {
                reportCommand = value;
                OnPropertyChanged("ReportCommand");
            }
        }

        public int SelectedTabIndex {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (getWeekIndex(selectedDay) < value)
                {
                    selectedDay = selectedDay.AddDays(value - getWeekIndex(selectedDay));
                }
                else
                {
                    selectedDay = selectedDay.AddDays(-(getWeekIndex(selectedDay) - value));
                }
                SelectedReport = new Report();
                selectedIndex = value;
            }
        }

        public ObservableCollection<Report> MondayReports
        {
            get { return mondayReports; }
            set {
                mondayReports = value;
                OnPropertyChanged("MondayReports");
            }
        }

        public ObservableCollection<Report> TuesdayReports
        {
            get { return tuesdayReports; }
            set {
                tuesdayReports = value;
                OnPropertyChanged("TuesdayReports");
            }
        }

        public ObservableCollection<Report> WednesdayReports
        {
            get { return wednwsdayReports; }
            set {
                wednwsdayReports = value;
                OnPropertyChanged("WednesdayReports");
            }
        }

        public ObservableCollection<Report> ThursdayReports
        {
            get { return thursdayReports; }
            set {
                thursdayReports = value;
                OnPropertyChanged("ThursdayReports");
            }
        }

        public ObservableCollection<Report> FridayReports
        {
            get { return fridayReports; }
            set {
                fridayReports = value;
                OnPropertyChanged("FridayReports");
            }
        }

        public ObservableCollection<Report> SaturdayReports
        {
            get { return saturdayReports; }
            set {
                saturdayReports = value;
                OnPropertyChanged("SaturdayReports");
            }
        }

        public ObservableCollection<Report> SundayReports
        {
            get { return sundayReports; }
            set {
                sundayReports = value;
                OnPropertyChanged("SundayReports");
            }
        }

        public ObservableCollection<Project> Projects 
        {
            get { return projects; }
            set { projects = value; }
        }

        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        public string SelectedMonth
        {
            get { return selectedMonth; }
            set
            {
                selectedMonth = value;
                OnPropertyChanged("SelectedMonth");
            }
        }

        public string MondayHeader
        {
            get { return mondayHeader; }
            set
            {
                mondayHeader = value;
                OnPropertyChanged("MondayHeader");
            }
        }


        public string TuesdayHeader
        {
            get { return tuesdayHeader; }
            set
            {
                tuesdayHeader = value;
                OnPropertyChanged("TuesdayHeader");
            }
        }


        public string WednesdayHeader
        {
            get { return wednesdayHeader; }
            set
            {
                wednesdayHeader = value;
                OnPropertyChanged("WednesdayHeader");
            }
        }

        public string ThursdayHeader
        {
            get { return thursdayHeader; }
            set
            {
                thursdayHeader = value;
                OnPropertyChanged("ThursdayHeader");
            }
        }


        public string FridayHeader
        {
            get { return fridayHeader; }
            set
            {
                fridayHeader = value;
                OnPropertyChanged("FridayHeader");
            }
        }


        public string SaturdayHeader
        {
            get { return saturdayHeader; }
            set
            {
                saturdayHeader = value;
                OnPropertyChanged("SaturdayHeader");
            }
        }


        public string SundayHeader
        {
            get { return sundayHeader; }
            set
            {
                sundayHeader = value;
                OnPropertyChanged("SundayHeader");
            }
        }

        public double MondayProgress
        {
            get { return mondayProgress; }
            set
            {
                mondayProgress = value;
                OnPropertyChanged("MondayProgress");
            }
        }


        public double TuesdayProgress
        {
            get { return tuesdayProgress; }
            set
            {
                tuesdayProgress = value;
                OnPropertyChanged("TuesdayProgress");
            }
        }


        public double WednesdayProgress
        {
            get { return wednesdayProgress; }
            set
            {
                wednesdayProgress = value;
                OnPropertyChanged("WednesdayProgress");
            }
        }

        public double ThursdayProgress
        {
            get { return thursdayProgress; }
            set
            {
                thursdayProgress = value;
                OnPropertyChanged("ThursdayProgress");
            }
        }


        public double FridayProgress
        {
            get { return fridayProgress; }
            set
            {
                fridayProgress = value;
                OnPropertyChanged("FridayProgress");
            }
        }


        public double SaturdayProgress
        {
            get { return saturdayProgress; }
            set
            {
                saturdayProgress = value;
                OnPropertyChanged("SaturdayProgress");
            }
        }


        public double SundayProgress
        {
            get { return sundayProgress; }
            set
            {
                sundayProgress = value;
                OnPropertyChanged("SundayProgress");
            }
        }

        //public ObservableCollection<Task> ActivityEnum
        //{
        //    get { return tasks; }
        //    set { tasks = value; }
        //}


        public IEnumerable<Activity> ActivityEnum
        {
            get
            {
                return Enum.GetValues(typeof(Activity)).Cast<Activity>();
            }
        }

        public RelayCommand AddReport
        {
            get
            {
                return addReport ??
                    (addReport = new RelayCommand(obj =>
                    {
                        if (SelectedReport.Equals(reportRepo.get(SelectedReport.Id)))
                        {
                            reportRepo.update(SelectedReport);
                            SelectedReport = new Report();
                        }
                        else if (validTimeCheck(SelectedReport.StartHours, SelectedReport.EndHours))
                        {
                            if (SelectedReport.Activity == Activity.TimeOff)
                            {
                                if ((SelectedReport.EndHours - SelectedReport.StartHours).TotalHours > (double)8)
                                {
                                    currentEmpl.TimeOff = currentEmpl.TimeOff - 1;
                                }
                                else
                                {
                                    currentEmpl.TimeOff = currentEmpl.TimeOff - (double)((SelectedReport.EndHours - SelectedReport.StartHours).TotalHours) / 8;
                                }
                            }
                            else if (SelectedReport.Activity == Activity.Sickness)
                            {
                                if ((SelectedReport.EndHours - SelectedReport.StartHours).TotalHours > (double)8)
                                {
                                    currentEmpl.Sickness = currentEmpl.Sickness - 1;
                                }
                                else
                                {
                                    currentEmpl.Sickness = currentEmpl.Sickness - (double)((SelectedReport.EndHours - SelectedReport.StartHours).TotalHours) / 8;
                                }
                            }
                            else
                            {
                                currentEmpl.TimeOff = currentEmpl.TimeOff + timeOffFromReport(SelectedReport.StartHours, selectedReport.EndHours);
                            }
                            
                            Report report = reportRepo.save(new Report
                            {
                                Project = SelectedReport.Project,
                                Task = SelectedReport.Task,
                                Activity = SelectedReport.Activity,
                                EmployeeId = currentEmpl.Id,
                                StartHours = SelectedReport.StartHours,
                                EndHours = SelectedReport.EndHours,
                                Date = selectedDay
                            });
                            addToCollection(getWeekIndex(selectedDay), report);
                            SelectedReport = new Report();
                            
                        }
                        employeeRepo.update(currentEmpl);
                        setWeekProgress();
                    }));
            }
        }

        public RelayCommand DeleteReport
        {
            get
            {
                return deleteReport ??
                    (deleteReport = new RelayCommand(obj =>
                    {
                        if (!(SelectedReport.Id == 0))
                        {
                            Report report = reportRepo.delete(SelectedReport);
                            getDayCollection(SelectedTabIndex).Remove(SelectedReport);
                            SelectedReport = new Report();
                        }
                        currentEmpl.TimeOff = currentEmpl.TimeOff - timeOffFromReport(selectedReport.StartHours, selectedReport.EndHours);
                        employeeRepo.update(currentEmpl);
                        setWeekProgress();
                    }
                    ));
            }
        }

        public RelayCommand NextWeek
        {
            get
            {
                return nextWeek ??
                    (nextWeek = new RelayCommand(obj =>
                    {
 
                        selectedDay =  selectedDay.AddDays(7);
                        SelectedTabIndex = getWeekIndex(selectedDay);
                        setWeekCollection(selectedDay);
                        setWeekRange(selectedDay);
                    }
                    ));
            }
        }

        public RelayCommand PreviousWeek
        {
            get
            {
                return previousWeek ??
                    (previousWeek = new RelayCommand(obj =>
                    {
                        selectedDay = selectedDay.AddDays(-7);
                        SelectedTabIndex = getWeekIndex(selectedDay);
                        setWeekCollection(selectedDay);
                        setWeekRange(selectedDay);
                    }
                    ));
            }
        }

        public RelayCommand Info
        {
            get
            {
                return info ??
                    (info = new RelayCommand(obj =>
                    {
                        Employee_Info ei = new Employee_Info(dbContext, currentEmpl);
                        ei.ShowDialog();
                    }
                    ));
            }
        }

        public RelayCommand AdminPanel
        {
            get
            {
                return adminPanel ??
                    (adminPanel = new RelayCommand(obj =>
                    {
                        ControlPanel cp = new ControlPanel(dbContext, currentEmpl);
                        cp.ShowDialog();
                    }
                    ));
            }
        }


        public ReportingViewModel(DBContext context, Employee empl)
        {
            dbContext = context;
            employeeRepo = new EmployeeImpl(dbContext);
            projectRepo = new ProjectImpl(dbContext);
            reportRepo = new ReportImpl(dbContext);
            taskRepo = new TaskImpl(dbContext);
            currentEmpl = empl;
            selectedDay = DateTime.Now.Date;
            setWeekCollection(selectedDay);
            setWeekRange(selectedDay);
            SelectedTabIndex = getWeekIndex(selectedDay);
            SelectedReport = new Report();

            if (empl.ProjectId != null)
            {
                Project pr = projectRepo.get((int)empl.ProjectId);
                Projects.Add(pr);
                Tasks = new ObservableCollection<Task>(taskRepo.getAll().Where(task => task.ProjectId == pr.Id));
            }

        }

        private void setWeekCollection(DateTime date)
        {
            MondayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay)).Date && report.EmployeeId == currentEmpl.Id));
            TuesdayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 1).Date && report.EmployeeId == currentEmpl.Id));
            WednesdayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 2).Date && report.EmployeeId == currentEmpl.Id));
            ThursdayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 3).Date && report.EmployeeId == currentEmpl.Id));
            FridayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 4).Date && report.EmployeeId == currentEmpl.Id));
            SaturdayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 5).Date && report.EmployeeId == currentEmpl.Id));
            SundayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 6).Date && report.EmployeeId == currentEmpl.Id));

        }

        private void setWeekRange(DateTime day)
        {
            int dayOfWeek = getWeekIndex(day);
            DateTime MondayDay = day.AddDays(-dayOfWeek);
            DateTime SundayDay = day.AddDays(+6 - dayOfWeek);
            if (day.AddDays(-dayOfWeek).Month == day.AddDays(+6 - dayOfWeek).Month)
            {
                SelectedMonth = string.Format("{0} {1} - {2}, {3}",
                    day.ToString("MMMM", CultureInfo.InvariantCulture),
                    MondayDay.Day,
                    SundayDay.Day,
                    day.Year);
            }
            else
            {
                SelectedMonth = string.Format("{0} {1} - {2} {3}, {4}",
                    day.AddDays(-dayOfWeek).ToString("MMMM", CultureInfo.InvariantCulture),
                    MondayDay.Day,
                    day.AddDays(+6 - dayOfWeek).ToString("MMMM", CultureInfo.InvariantCulture),
                    SundayDay.Day,
                    day.Year);
            }
            MondayHeader = string.Format("{0} {1}", day.AddDays( - dayOfWeek).Day, "Monday");
            TuesdayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +1).Day, "Tuesday");
            WednesdayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +2).Day, "Wednesday");
            ThursdayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +3).Day, "Thursday");
            FridayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +4).Day, "Friday");
            SaturdayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +5).Day, "Saturday");
            SundayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +6).Day, "Sunday");
            setWeekProgress();
        }

        private void setWeekProgress()
        {
            MondayProgress = getWorkedTime(MondayReports);
            TuesdayProgress = getWorkedTime(TuesdayReports);
            WednesdayProgress = getWorkedTime(WednesdayReports);
            ThursdayProgress = getWorkedTime(ThursdayReports);
            FridayProgress = getWorkedTime(FridayReports);
            SaturdayProgress = getWorkedTime(SaturdayReports);
            SundayProgress = getWorkedTime(SundayReports);
        }

        private double getWorkedTime(ObservableCollection<Report> collection)
        {
            double result = 0.0;
            foreach (Report report in collection)
            {
                result = result + (report.EndHours - report.StartHours).TotalHours;
            }
            return result;
        }

        private int getWeekIndex(DateTime date)
        {
            return (int)(date.DayOfWeek + 6) % 7;
        }

        private double timeOffFromReport(TimeSpan Start, TimeSpan End)
        {
            //List<Report> reports = new List<Report>();
            //reports.AddRange(reportRepo.getAll().Where(report => report.EmployeeId == currentEmpl.Id));
            //TimeSpan result = new TimeSpan();
            //foreach (Report report in reports)
            //{
            //    result += report.EndHours - report.StartHours;
            //}
            double timeOffInHour = (double)18 / 2016;
            double workedHours = (End-Start).TotalHours;
            return Math.Round(workedHours * timeOffInHour, 4);
        }

        private bool validTimeCheck(TimeSpan startTS, TimeSpan endTS)
        {
            if (TimeSpan.Compare(startTS, endTS) == 1)
            {
                MessageBox.Show("Start time is greater than end time!");
                return false;
            }
            if (TimeSpan.Compare(startTS, endTS) == 0)
            {
                MessageBox.Show("Start and end times are the same!");
                return false;
            }

            foreach (Report report in getDayCollection(SelectedTabIndex))
            {
                if (betweenTS(startTS, report.StartHours, report.EndHours))
                {
                    MessageBox.Show("Selected time is already reported!");
                    return false;
                }
                if (betweenTS(endTS, report.StartHours, report.EndHours))
                {
                    MessageBox.Show("Selected time is already reported!");
                    return false;
                }
            }

            return true;
        }

        private void addToCollection(int index, Report report)
        {
            switch (index)
            {
                case 0:
                    MondayReports.Add(report);
                    break;
                case 1:
                    TuesdayReports.Add(report);
                    break;
                case 2:
                    WednesdayReports.Add(report);
                    break;
                case 3:
                    ThursdayReports.Add(report);
                    break;
                case 4:
                    FridayReports.Add(report);
                    break;
                case 5:
                    SaturdayReports.Add(report);
                    break;
                case 6:
                    SundayReports.Add(report);
                    break;
            }
        }

        private ObservableCollection<Report> getDayCollection(int index)
        {
            switch (index)
            {
                case 0: return MondayReports;
                case 1: return TuesdayReports;
                case 2: return WednesdayReports;
                case 3: return ThursdayReports;
                case 4: return FridayReports;
                case 5: return SaturdayReports;
                case 6: return SundayReports;
            }
            return null;
        }

        private bool betweenTS(TimeSpan ts, TimeSpan top, TimeSpan bot)
        {
            if ((TimeSpan.Compare(ts, bot) == 1 && TimeSpan.Compare(ts, top) == -1) || (TimeSpan.Compare(ts, bot) == -1 && TimeSpan.Compare(ts, top) == 1))
            {
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public Action Close { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
                if (employeeRepo != null)
                {
                    employeeRepo.Dispose();
                    employeeRepo = null;
                }
                if (projectRepo != null)
                {
                    projectRepo.Dispose();
                    projectRepo = null;
                }
                if (reportRepo != null)
                {
                    reportRepo.Dispose();
                    reportRepo = null;
                }
                if (taskRepo != null)
                {
                    taskRepo.Dispose();
                    taskRepo = null;
                }
            }
        }
    }
}
