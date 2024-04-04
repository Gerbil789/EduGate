using Database.Models;
using System.Windows;

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
            DialogResult = true;
            this.Close();
        }
    }
}
