using Database.Models;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApp.Windows
{
    public partial class StudentForm : Window
    {
        public Student Student { get; private set; }


        //edit student
        public StudentForm(Student? student = null)
        {
            InitializeComponent();
            Student = new Student();
            if (student != null)
            {
                Title = "Edit student";
                ConfirmButton.Content = "Uložit";
                Student.StudentId = student.StudentId;
                Student.FirstName = student.FirstName;
                Student.LastName = student.LastName;
                Student.BirthDate = student.BirthDate;
                Student.Address.Street = student.Address.Street;
                Student.Address.Number = student.Address.Number;
                Student.Address.City = student.Address.City;
                Student.Address.State = student.Address.State;
                Student.Address.ZipCode = student.Address.ZipCode;
                Student.Email = student.Email;
                Student.Phone.Code = student.Phone.Code;
                Student.Phone.Number = student.Phone.Number;

            }
            else
            {
                Title = "Add student";
                ConfirmButton.Content = "Přidat";
            }

           
            this.DataContext = Student;
        }

        private void SaveStudent(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
