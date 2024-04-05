using Database.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApp
{
    public partial class MainWindow : Window
    {

        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>
    {
      new Student
      {
        StudentId = 1,
        FirstName = "Vojtěch",
        LastName = "Rubeš",
        BirthDate = new System.DateTime(2001, 11, 1),
        Address = new Address
        {
          Street = "Komenského",
          Number = "48",
          City = "Lhota u Opavy",
          State = "Česká Republika",
          ZipCode = "74792"
        },
        Email = "vojta.rubes.01@gmail.com",
        Phone = "603 197 038"

      },
      new Student
      {
        StudentId = 2,
        FirstName = "Jane",
        LastName = "Doe",
        BirthDate = new System.DateTime(2001, 11, 1),
        Address = new Address
        {
          Street = "123 Main St",
          City = "Springfield",
          State = "CZ",
          ZipCode = "62701"
        },
        Email = ""
      }
    };
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            StudentForm form = new();

            if (form.ShowDialog() == true)
            {
                Students.Add(form.NewStudent);
            }
        }

        private void DeleteStudent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var student = (Student)button.DataContext;
            Students.Remove(student);
        }

        private void EditStudent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var student = (Student)button.DataContext;

            StudentForm form = new(new(student));
            if (form.ShowDialog() == true)
            {
                student.Update(form.NewStudent);
            }
        }

        private void AnonymeStudent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var student = (Student)button.DataContext;
            student.FirstName = "bruh";

        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string searchTerm = textBox.Text.ToLower(); // Convert search term to lowercase for case-insensitive search

            // Filter the Students collection based on the search term
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
                                                                            || student.Address.ZipCode.ToLower().Contains(searchTerm)))
                                            || (!string.IsNullOrEmpty(student.Email) && student.Email.ToLower().Contains(searchTerm))
                                            || (!string.IsNullOrEmpty(student.Phone) && student.Phone.ToLower().Contains(searchTerm)));
                };
            }
            else
            {
                view.Filter = null; // Remove any existing filter
            }
        }
    }
}