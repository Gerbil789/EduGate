using Database.Models;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApp.Windows
{
    public partial class StudentForm : Window
    {
        public Student Student { get; private set; }



        //edit student
        public StudentForm(MainWindow parent, Student? student = null)
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
                Student.Application1 = student.Application1;
                Student.Application2 = student.Application2;
                Student.Application3 = student.Application3;

            }
            else
            {
                Title = "Add student";
                ConfirmButton.Content = "Přidat";
            }
            school1.ItemsSource = parent.Schools;
            school2.ItemsSource = parent.Schools;
            school3.ItemsSource = parent.Schools;

         
            school1.SelectedValue = parent.Schools.FirstOrDefault(x => x.StudyPrograms.Contains(Student.Application1.StudyProgram));
            school2.SelectedValue = parent.Schools.FirstOrDefault(x => x.StudyPrograms.Contains(Student.Application2.StudyProgram));
            school3.SelectedValue = parent.Schools.FirstOrDefault(x => x.StudyPrograms.Contains(Student.Application3.StudyProgram));

            program1.ItemsSource = ((School)school1.SelectedValue)?.StudyPrograms ?? null;
            program2.ItemsSource = ((School)school2.SelectedValue)?.StudyPrograms ?? null;
            program3.ItemsSource = ((School)school3.SelectedValue)?.StudyPrograms ?? null;

            program1.SelectedValue = Student.Application1.StudyProgram;
            program2.SelectedValue = Student.Application2.StudyProgram;
            program3.SelectedValue = Student.Application3.StudyProgram;



            this.DataContext = Student;
        }

        private void SaveStudent(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
            this.Close();
        }

        private void SchoolSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if(comboBox.SelectedItem == null)
            {
                return;
            }
           
            var comboBoxName = comboBox.Name;

            switch(comboBoxName)
            {
                case "school1":
                    program1.ItemsSource = ((School)comboBox.SelectedItem).StudyPrograms;
                    break;
                case "school2":
                    program2.ItemsSource = ((School)comboBox.SelectedItem).StudyPrograms;
                    break;
                case "school3":
                    program3.ItemsSource = ((School)comboBox.SelectedItem).StudyPrograms;
                    break;
            }
        }

        private void ProgramSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var comboBoxName = comboBox.Name;

            switch (comboBoxName)
            {
                case "program1":
                    Student.Application1.StudyProgram = (StudyProgram)comboBox.SelectedItem;
                    break;
                case "program2":
                    Student.Application2.StudyProgram = (StudyProgram)comboBox.SelectedItem;
                    break;
                case "program3":
                    Student.Application3.StudyProgram = (StudyProgram)comboBox.SelectedItem;
                    break;
            }
        }

        private void ResetStudyProgram(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var buttonName = button.Name;

            switch (buttonName)
            {
                case "reset1":

                    program1.SelectedValue = -1;
                    school1.SelectedValue = -1;
                    break;
                case "reset2":
                    program2.SelectedValue = -1;
                    school2.SelectedValue = -1;
                    break;
                case "reset3":
                    program3.SelectedValue = -1;
                    school3.SelectedValue = -1;
                    break;
            }
        }
    }
}
