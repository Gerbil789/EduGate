using Database.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Database.Repositories;

namespace DesktopApp
{

    public partial class MainWindow : Window
    {
        private readonly DatabaseRepository repository;
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<School> Schools { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            repository = new DatabaseRepository();

            Initialize().Wait();
        }

        async Task Initialize()
        {
            Students = new ObservableCollection<Student>(await repository.GetStudents());
            Schools = new ObservableCollection<School>(await repository.GetSchools());
        }

        private async void AddStudent(object sender, RoutedEventArgs e)
        {
            StudentForm form = new();
            form.Owner = this;
            form.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (form.ShowDialog() == true)
            {
                if (await repository.AddStudent(form.Student))
                {
                    Students.Add(form.Student);
                }
            }
        }

        private async void DeleteStudent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var student = (Student)button.DataContext;

            ConfirmWindow confirm = new($"Delete student {student}");
            confirm.Owner = this;
            confirm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (confirm.ShowDialog() == true)
            {
                if (await repository.DeleteStudent(student))
                {
                    Students.Remove(student);
                }
            }
        }

        private async void EditStudent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var student = (Student)button.DataContext;

            StudentForm form = new(student);
            form.Owner = this;
            form.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (form.ShowDialog() == true)
            {
                if (await repository.UpdateStudent(form.Student))
                {

                }
                else
                {
                    NotificationWindow notification = new("Failed to update student");
                    notification.Owner = this;
                    notification.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    notification.ShowDialog();
                }

            }
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string searchTerm = textBox.Text.ToLower();

            ICollectionView view = CollectionViewSource.GetDefaultView(Students);
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                view.Filter = item =>
                {
                    Student student = (Student)item;
                    return student != null && (student.FirstName.ToLower().Contains(searchTerm)
                                            || student.LastName.ToLower().Contains(searchTerm)
                                            || (student.Address != null && (student.Address.Street.ToLower().Contains(searchTerm)
                                                    || student.Address.City.ToLower().Contains(searchTerm)
                                                    || student.Address.State.ToLower().Contains(searchTerm)
                                                    || student.Address.ZipCode.ToLower().Contains(searchTerm))));
                };
            }
            else
            {
                view.Filter = null;
            }
        }
    }
}