using Database.Models;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApp
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
            }

           
            this.DataContext = Student;
        }

        private void SaveStudent(object sender, RoutedEventArgs e)
        {
            //ConfirmWindow confirm = new("Confirm changes");
            //confirm.Owner = this;
            //confirm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //if (confirm.ShowDialog() == true)
            //{
            //    DialogResult = true;
            //    this.Close();
            //}

            DialogResult = true;
            this.Close();


        }
    }
}
