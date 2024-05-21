using DesktopApp.Models;
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
                Student.Code = student.Code;
                Student.Number = student.Number;
                Student.Applications = student.Applications;

            }
            else
            {
                Title = "Add student";
                ConfirmButton.Content = "Přidat";
                Student.Applications.Add(new Models.Application() { Student = this.Student });
                Student.Applications.Add(new Models.Application() { Student = this.Student });
                Student.Applications.Add(new Models.Application() { Student = this.Student });
            }
            school1.ItemsSource = parent.Schools;
            school2.ItemsSource = parent.Schools;
            school3.ItemsSource = parent.Schools;

            if (Student.Applications[0]?.StudyProgram?.StudyProgramId != 0)
            {
                var program = Student.Applications[0].StudyProgram;
                if (program != null)
                {
                    var schoolId = parent.db.GetSchoolIdByStudyProgram(program.StudyProgramId).Result;
                    var school = parent.Schools.FirstOrDefault(x => x.SchoolId == schoolId);
                    school1.SelectedValue = school;

                    program1.ItemsSource = school?.StudyPrograms ?? null;
                    program1.SelectedValue = school?.StudyPrograms.FirstOrDefault(x => x.StudyProgramId == program.StudyProgramId);
                }
            }

            if (Student.Applications[1]?.StudyProgram?.StudyProgramId != 0)
            {
                var program = Student.Applications[1].StudyProgram;
                if (program != null)
                {
                    var schoolId = parent.db.GetSchoolIdByStudyProgram(program.StudyProgramId).Result;
                    var school = parent.Schools.FirstOrDefault(x => x.SchoolId == schoolId);
                    school2.SelectedValue = school;

                    program2.ItemsSource = school?.StudyPrograms ?? null;
                    program2.SelectedValue = school?.StudyPrograms.FirstOrDefault(x => x.StudyProgramId == program.StudyProgramId);
                }
            }

            if (Student.Applications[2]?.StudyProgram?.StudyProgramId != 0)
            {
                var program = Student.Applications[2].StudyProgram;
                if (program != null)
                {
                    var schoolId = parent.db.GetSchoolIdByStudyProgram(program.StudyProgramId).Result;
                    var school = parent.Schools.FirstOrDefault(x => x.SchoolId == schoolId);
                    school3.SelectedValue = school;

                    program3.ItemsSource = school?.StudyPrograms ?? null;
                    program3.SelectedValue = school?.StudyPrograms.FirstOrDefault(x => x.StudyProgramId == program.StudyProgramId);
                }
            }

        

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
            var school = (School)comboBox.SelectedItem;

            switch(comboBoxName)
            {
                case "school1":
                    program1.ItemsSource = school.StudyPrograms;
                    break;
                case "school2":
                    program2.ItemsSource = school.StudyPrograms;
                    break;
                case "school3":
                    program3.ItemsSource = school.StudyPrograms;
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
                    Student.Applications[0].StudyProgram = (StudyProgram)comboBox.SelectedItem;
                    break;
                case "program2":
                    Student.Applications[1].StudyProgram = (StudyProgram)comboBox.SelectedItem;
                    break;
                case "program3":
                    Student.Applications[2].StudyProgram = (StudyProgram)comboBox.SelectedItem;
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
