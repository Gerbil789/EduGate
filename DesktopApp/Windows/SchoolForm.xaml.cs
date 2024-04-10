using Database.Models;
using System.Windows;

namespace DesktopApp.Windows
{
    public partial class SchoolForm : Window
    {
        public School School { get; set; } = new();

        public SchoolForm(School? school = null)
        {
            InitializeComponent();

            if(school == null)
            {
                Title = "Add School";
                ConfirmButton.Content = "Přidat";
            }
            else
            {
                Title = "Edit School";
                ConfirmButton.Content = "Uložit";

                School.SchoolId = school.SchoolId;
                School.Name = school.Name;
                School.Address = school.Address;
                School.Address.Street = school.Address.Street;
                School.Address.Number = school.Address.Number;
                School.Address.City = school.Address.City;
                School.Address.State = school.Address.State;
                School.Address.ZipCode = school.Address.ZipCode;
                School.StudyPrograms = school.StudyPrograms;



            }
            this.DataContext = School;

        }

        private void AddProgram(object sender, RoutedEventArgs e)
        {
            try
            {
                var form = new StudyProgramForm();
                form.Owner = this;
                form.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                if (form.ShowDialog() == false) return;


                School.StudyPrograms.Add(form.StudyProgram);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }



        private void Save(object sender, RoutedEventArgs e)
        {
            

            DialogResult = true;
            this.Close();
        }
    }
}
