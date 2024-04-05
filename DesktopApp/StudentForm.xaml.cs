using Database.Models;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApp
{
    public partial class StudentForm : Window
    {
        public Student NewStudent { get; private set; }
        //add student
        public StudentForm()
        {
            InitializeComponent();
            NewStudent = new Student();
            this.DataContext = NewStudent;
        }

        //edit student
        public StudentForm(Student student)
        {
            InitializeComponent();
            NewStudent = student;
            this.DataContext = NewStudent;
        }

        private void SaveStudent(object sender, RoutedEventArgs e)
        {
            ConfirmWindow confirm = new("Confirm changes");
            confirm.Owner = this;
            confirm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (confirm.ShowDialog() == true)
            {
                DialogResult = true;
                this.Close();
            }

           
        }
    }
}
