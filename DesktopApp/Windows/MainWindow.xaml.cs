using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NewDatabase;
using DesktopApp.Models;

namespace DesktopApp.Windows
{
    public partial class MainWindow : Window
    {
        private TextLogger logger;
        private ObservableCollection<string> logEntries = new ObservableCollection<string>();
        public readonly Database db;
        public ObservableCollection<Student> Students { get; set; } = new();
        public ObservableCollection<School> Schools { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
            
            db = new();
            Initialize().Wait();
            this.DataContext = this;

            logger = new TextLogger("log.txt", LogEntryAdded);
        }

        public void LogEntryAdded(string logMessage)
        {
            Dispatcher.Invoke(() =>
            {
                logEntries.Add(logMessage);
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            logger.Stop(); // Ensure logger is stopped gracefully
        }

        async Task Initialize()
        {
            try
            {
                Students = new ObservableCollection<Student>(await db.GetStudents());
                Schools = new ObservableCollection<School>(await db.GetSchools());
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string searchTerm = textBox.Text.ToLower();

            var item = (TabItem)Tabs.SelectedItem;

            ICollectionView view;
            switch (item.Tag)
            {
                case "students":
                    view = CollectionViewSource.GetDefaultView(Students);
                    if (string.IsNullOrWhiteSpace(searchTerm))
                    {
                        view.Filter = null;
                        return;
                    }
                    view.Filter = item =>
                    {
                        Student student = (Student)item;
                        return student != null && (student.FirstName.ToLower().Contains(searchTerm)
                                                || student.LastName.ToLower().Contains(searchTerm)
                                                || (student.Address != null &&
                                                    (student.Address.Street.ToLower().Contains(searchTerm)
                                                        || student.Address.City.ToLower().Contains(searchTerm)
                                                        || student.Address.State.ToLower().Contains(searchTerm))));
                    };
                    break;
                case "schools":
                    view = CollectionViewSource.GetDefaultView(Schools);
                    if (string.IsNullOrWhiteSpace(searchTerm))
                    {
                        view.Filter = null;
                        return;
                    }
                    view.Filter = item =>
                    {
                        School school = (School)item;
                        return school != null && (school.Name.ToLower().Contains(searchTerm)
                                                || (school.Address != null &&
                                                    (school.Address.Street.ToLower().Contains(searchTerm)
                                                        || school.Address.City.ToLower().Contains(searchTerm)
                                                        || school.Address.State.ToLower().Contains(searchTerm))));
                    };
                    break;
            }
        }

        //--------------------Students--------------------
        private async void AddStudent(object sender, RoutedEventArgs e)
        {
            StudentForm form = new(this);
            form.Owner = this;
            form.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (form.ShowDialog() == false) return;

            try
            {
                await db.AddStudent(form.Student);
                Students.Add(form.Student);
                logger.Log($"Student {form.Student} added");
            }
            catch (Exception ex)
            {
                logger.Log($"Error adding student {form.Student}: {ex.Message}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void DeleteStudent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var student = (Student)button.DataContext;

            if (MessageBox.Show($"Potvrdit trvalé odstranění {student}?", "Student", MessageBoxButton.OKCancel, MessageBoxImage.Warning) != MessageBoxResult.OK)
            {
                return;
            }

            try
            {
                await db.DeleteStudent(student);
                Students.Remove(student);
                logger.Log($"Student {student} deleted");
            }
            catch (Exception ex)
            {
                logger.Log($"Error deleting student {student}: {ex.Message}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void EditStudent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var student = (Student)button.DataContext;

            StudentForm form = new(this, student);
            form.Owner = this;
            form.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (form.ShowDialog() == false) return;

            try
            {
                await db.UpdateStudent(form.Student);
                logger.Log($"Student {form.Student} updated");
            }
            catch (Exception ex)
            {
                logger.Log($"Error updating student {student}: {ex.Message}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //--------------------Schools--------------------
        private async void AddSchool(object sender, RoutedEventArgs e)
        {
            SchoolForm form = new();
            form.Owner = this;
            form.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (form.ShowDialog() == false) return;

            try
            {
                await db.AddSchool(form.School);
                Schools.Add(form.School);
                logger.Log($"School {form.School} added");
            }
            catch (Exception ex)
            {
                logger.Log($"Error adding school {form.School}: {ex.Message}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void EditSchool(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                var school = (School)button.DataContext;

                SchoolForm form = new(school);
                form.Owner = this;
                form.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                if (form.ShowDialog() == false) return;

                await db.UpdateSchool(form.School);
                Schools[Schools.IndexOf(school)] = form.School;
                logger.Log($"School {form.School} updated");
                
            }
            catch (Exception ex)
            {
                logger.Log($"Error updating school: {ex.Message}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteSchool(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var school = (School)button.DataContext;

            if (MessageBox.Show($"Odstranit {school}?", "School", MessageBoxButton.OKCancel, MessageBoxImage.Warning) != MessageBoxResult.OK)
            {
                return;
            }

            try
            {
                await db.DeleteSchool(school);
                Schools.Remove(school);
                logger.Log($"School {school} deleted");
            }
            catch (Exception ex)
            {
                logger.Log($"Error deleting school {school}: {ex.Message}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}