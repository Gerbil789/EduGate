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
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>
    {
      new Student
      {
        StudentId = 1,
        FirstName = "John",
        LastName = "Doe",
        Address = new Address
        {
          Street = "123 Main St",
          City = "Springfield",
          State = "IL",
          ZipCode = "62701"
        },
        Email = ""
      },
      new Student
      {
        StudentId = 2,
        FirstName = "Jane",
        LastName = "Doe",
        Address = new Address
        {
          Street = "123 Main St",
          City = "Springfield",
          State = "IL",
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
      Student student = new();
      StudentForm form = new StudentForm(student);
      form.ShowDialog();

      if (form.DialogResult == true)
      {
        Students.Add(student);
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

      StudentForm form = new(student);
      form.ShowDialog();
    }

    private void AnonymeStudent(object sender, RoutedEventArgs e)
    {
      Button button = (Button)sender;
      var student = (Student)button.DataContext;
      student.FirstName = "bruh";

    }
  }
}