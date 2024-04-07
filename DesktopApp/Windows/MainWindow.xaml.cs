using Database.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Database.Repositories;

namespace DesktopApp.Windows
{

    public partial class MainWindow : Window
    {
        private readonly DatabaseRepository repository;
        public ObservableCollection<Student> Students { get; set; } = new();
        public ObservableCollection<School> Schools { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            repository = new DatabaseRepository();
            Initialize().Wait();
        }
        async Task Initialize()
        {
            try
            {
                Students = new ObservableCollection<Student>(await repository.GetStudents());
                Schools = new ObservableCollection<School>(await repository.GetSchools());
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
            StudentForm form = new();
            form.Owner = this;
            form.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (form.ShowDialog() == false) return;

            try
            {
                if (await repository.AddStudent(form.Student))
                {
                    Students.Add(form.Student);
                }
            }
            catch (Exception ex)
            {
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
                if (await repository.DeleteStudent(student))
                {
                    Students.Remove(student);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void EditStudent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var student = (Student)button.DataContext;

            StudentForm form = new(student);
            form.Owner = this;
            form.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (form.ShowDialog() == false) return;

            try
            {
                await repository.UpdateStudent(form.Student);
            }
            catch (Exception ex)
            {
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
                if (await repository.AddSchool(form.School))
                {
                    Schools.Add(form.School);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void EditSchool(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var school = (School)button.DataContext;

            SchoolForm form = new(school);
            form.Owner = this;
            form.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (form.ShowDialog() == false) return;

            try
            {
                await repository.UpdateSchool(form.School);
            }
            catch (Exception ex)
            {
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
                if (await repository.DeleteSchool(school))
                {
                    Schools.Remove(school);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}