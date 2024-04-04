using Database.Models;
using System.Collections.ObjectModel;
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
    }
}