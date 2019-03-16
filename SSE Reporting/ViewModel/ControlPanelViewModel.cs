using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace SSE_Reporting.ViewModel
{
    class ControlPanelViewModel : INotifyPropertyChanged, IDisposable
    {
        private Employee selectedEmployee;
        private int employeeSelectedIndex;
        private Employee selectedAdmin;
        private int adminSelectedIndex;
        private Project selectedProject;
        private int projectSelectedIndex;
        private Task selectedTask;
        private int taskSelectedIndex;
        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        private ObservableCollection<Employee> admins = new ObservableCollection<Employee>();
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();

        public ObservableCollection<Employee> Employees {
            get { return employees; }
            set {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }
        public ObservableCollection<Employee> Admins {
            get { return admins; }
            set {
                admins = value;
                OnPropertyChanged("Admins");
            }
        }
        public ObservableCollection<Project> Projects {
            get { return projects; }
            set {
                projects = value;
                OnPropertyChanged("Projects");
            }
        }
        public ObservableCollection<Task> Tasks {
            get { return tasks; }
            set {
                tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        public int ProjectSelectedIndex
        {
            get { return projectSelectedIndex; }
            set { projectSelectedIndex = value; OnPropertyChanged("ProjectSelectedIndex"); }
        }

        public int TaskSelectedIndex
        {
            get { return taskSelectedIndex; }
            set { taskSelectedIndex = value; OnPropertyChanged("TaskSelectedIndex"); }
        }

        public int EmployeeSelectedIndex
        {
            get { return employeeSelectedIndex; }
            set { employeeSelectedIndex = value; OnPropertyChanged("EmployeeSelectedIndex"); }
        }

        public int AdminSelectedIndex
        {
            get { return adminSelectedIndex; }
            set { adminSelectedIndex = value; OnPropertyChanged("AdminSelectedIndex"); }
        }

        private RelayCommand deleteProject;
        private RelayCommand addProject;
        private RelayCommand editProject;
        private RelayCommand addTask;
        private RelayCommand deleteTask;
        private RelayCommand taskToProject;
        private RelayCommand addEmployee;
        private RelayCommand deleteEmployee;
        private RelayCommand makeAdmin;
        private RelayCommand employeeToProject;
        private RelayCommand addAdmin;
        private RelayCommand deleteAdmin;
        private RelayCommand pickUpAdmin;
        private RelayCommand adminToProject;

        private DBContext dbContext;

        IRepository<Employee> employeeRepo;
        //IRepository<Admin> adminRepo;
        IRepository<Project> projectRepo; 
        IRepository<Report> reportRepo; 
        IRepository<Task> taskRepo;

        public RelayCommand AddAdmin
        {
            get
            {
                return addAdmin ??
                    (addAdmin = new RelayCommand(obj =>
                    {
                        Employee admin = employeeRepo.save(new Admin { Login = SelectedAdmin.Login, Password = SelectedAdmin.Password });
                        Admins.Add(admin);
                        SelectedAdmin = new Employee();
                    }));
            }
        }

        public RelayCommand DeleteAdmin
        {
            get
            {
                return deleteAdmin ??
                    (deleteAdmin = new RelayCommand(obj =>
                    {
                        if (!(SelectedAdmin.Id == 0))
                        {
                            Employee admin = employeeRepo.delete(SelectedAdmin);
                            Admins.Remove(SelectedAdmin);
                            SelectedAdmin = new Employee();
                        }
                    }));
            }
        }

        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                    (addEmployee = new RelayCommand(obj =>
                    {
                        Employee employee = employeeRepo.save(new Employee { Login = SelectedEmployee.Login, Password = SelectedEmployee.Password });
                        Employees.Add(employee);
                        SelectedEmployee = new Employee();
                    }));
            }
        }


        public RelayCommand DeleteEmployee
        {
            get
            {
                return deleteEmployee ??
                    (deleteEmployee = new RelayCommand(obj =>
                    {
                        if (!SelectedEmployee.Equals(new Employee()))
                        {
                            Employee employee = employeeRepo.delete(SelectedEmployee);
                            Employees.Remove(SelectedEmployee);
                            SelectedEmployee = new Employee();
                        }

                    }));
            }
        }

        public RelayCommand MakeAdmin
        {
            get
            {
                return makeAdmin ??
                    (makeAdmin = new RelayCommand(obj =>
                    {
                        if (SelectedEmployee != null && !SelectedEmployee.Equals(new Employee()))
                        {
                            SelectedEmployee.Role = Role.Admin;
                            employeeRepo.update(SelectedEmployee);
                            SelectedEmployee = new Employee();
                            Employees = new ObservableCollection<Employee>(employeeRepo.getAll().Where(empl => empl.Role == Role.User));
                            Admins = new ObservableCollection<Employee>(employeeRepo.getAll().Where(empl => empl.Role == Role.Admin));
                            AdminSelectedIndex = -1;
                        }
                        else
                            MessageBox.Show("You didn't choose employee.");
                    }));
            }
        }

        public RelayCommand PickUpAdmin
        {
            get
            {
                return pickUpAdmin ??
                    (pickUpAdmin = new RelayCommand(obj =>
                    {
                        if (SelectedAdmin != null && !SelectedAdmin.Equals(new Admin()))
                        {
                            SelectedAdmin.Role = Role.User;
                            employeeRepo.update(SelectedAdmin);
                            SelectedAdmin = new Admin();
                            SelectedEmployee = new Employee();
                            Employees = new ObservableCollection<Employee>(employeeRepo.getAll().Where(empl => empl.Role == Role.User));
                            Admins = new ObservableCollection<Employee>(employeeRepo.getAll().Where(empl => empl.Role == Role.Admin));
                            EmployeeSelectedIndex = -1;
                        }
                        else
                            MessageBox.Show("You didn't choose admin.");
                    }));
            }
        }

        public RelayCommand AdminToProject
        {
            get
            {
                return adminToProject ??
                    (adminToProject = new RelayCommand(obj =>
                    {
                        if (SelectedProject != null && SelectedAdmin != null && !(SelectedProject.Id == 0) && !(SelectedAdmin.Id == 0))
                        {
                            SelectedAdmin.ProjectId = SelectedProject.Id;
                            SelectedProject.addEmployee(SelectedAdmin);
                            Employee employee = employeeRepo.update(SelectedAdmin);
                            Project project = projectRepo.update(SelectedProject);

                        }
                        else
                            MessageBox.Show("You didn't choose project or task.");

                    }));
            }
        }

        public RelayCommand EmployeeToProject
        {
            get
            {
                return employeeToProject ??
                    (employeeToProject = new RelayCommand(obj =>
                    {
                        if (SelectedProject != null && SelectedEmployee != null && !(SelectedProject.Id == 0) && !(SelectedEmployee.Id == 0))
                        {
                            SelectedEmployee.ProjectId = SelectedProject.Id;
                            SelectedProject.addEmployee(selectedEmployee);
                            Employee employee = employeeRepo.update(SelectedEmployee);
                            Project project = projectRepo.update(SelectedProject);
                     }
                        else
                            MessageBox.Show("You didn't choose project or task.");

                    }));
            }
        }

        public RelayCommand AddTask
        {
            get
            {
                return addTask ??
                    (addTask = new RelayCommand(obj =>
                    {

                        Task task = taskRepo.save(new Task { Name = selectedTask.Name, Activity = selectedTask.Activity });
                        Tasks.Add(task);
                        SelectedTask = new Task();
                    }));
            }
        }

        public RelayCommand DeleteTask
        {
            get
            {
                return deleteTask ??
                    (deleteTask = new RelayCommand(obj =>
                    {
                        if (!SelectedTask.Equals(new Task()))
                        {
                            Task task = taskRepo.delete(SelectedTask);
                            Tasks.Remove(SelectedTask);
                            SelectedTask = new Task();
                        }
                        
                    }));
            }
        }

        public RelayCommand TaskToProject
        {
            get
            {
                return taskToProject ??
                    (taskToProject = new RelayCommand(obj =>
                    {
                        if (SelectedProject != null && SelectedTask != null && !SelectedProject.Equals(new Project()) && !SelectedTask.Equals(new Task()))
                        {
                            SelectedProject.addTask(selectedTask);
                            Project project = projectRepo.update(SelectedProject);
                            
                        }
                        else
                            MessageBox.Show("You didn't choose project or task.");

                    }));
            }
        }

        public RelayCommand AddProject
        {
            get
            {
                return addProject ??
                    (addProject = new RelayCommand(obj =>
                    {
                                           
                        Project project = projectRepo.save(new Project{ Company = selectedProject.Company, Name = selectedProject.Name });
                        Projects.Add(project);
                        SelectedProject = project;
                    }));
            }
        }

        public RelayCommand DeleteProject
        {
            get
            {
                return deleteProject ??
                    (deleteProject = new RelayCommand(obj =>
                    {
                         Project project = projectRepo.delete(selectedProject);
                         Projects.Remove(SelectedProject);
                         SelectedProject = new Project();
                    }));
            }
        }

        public RelayCommand EditProject
        {
            get
            {
                return editProject ??
                    (editProject = new RelayCommand(obj =>
                    {

                        Project project = projectRepo.update(SelectedProject);
                        Projects = projectRepo.getAll();
                        SelectedProject = project;
                    }));
            }
        }

        public IEnumerable<Activity> ActivityEnum
        {
            get
            {
                return Enum.GetValues(typeof(Activity)).Cast<Activity>();
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

        public Employee SelectedAdmin
        {
            get { return selectedAdmin; }
            set
            {
                selectedAdmin = value;
                OnPropertyChanged("SelectedAdmin");
            }
        }

        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }

        public Task SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        public ControlPanelViewModel(DBContext context, Employee empl)
        {
            dbContext = context;
            employeeRepo = EmployeeImpl.getInstance(dbContext);
            //adminRepo = new AdminImpl(dbContext);
            projectRepo = ProjectImpl.getInstance(dbContext);
            reportRepo = ReportImpl.getInstance(dbContext);
            taskRepo = TaskImpl.getInstance(dbContext);

            Employees = new ObservableCollection<Employee>(employeeRepo.getAll().Where(user => user.Role == Role.User));
            Admins = new ObservableCollection<Employee>(employeeRepo.getAll().Where(user => user.Role == Role.Admin));
            Projects = projectRepo.getAll();
            Tasks = taskRepo.getAll();

            EmployeeSelectedIndex = -1;
            AdminSelectedIndex = -1;
            ProjectSelectedIndex = -1;
            TaskSelectedIndex = -1;

            SelectedProject = new Project();
            SelectedTask = new Task();
            SelectedEmployee = new Employee();
            SelectedAdmin = new Employee();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

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
