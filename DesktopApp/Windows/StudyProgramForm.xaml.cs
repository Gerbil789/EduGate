using DesktopApp.Models;
using System.Windows;

namespace DesktopApp.Windows
{
    public partial class StudyProgramForm : Window
    {
        public StudyProgram StudyProgram { get; set; } = new StudyProgram();
        public StudyProgramForm(StudyProgram? program = null)
        {
            InitializeComponent();
            if (program == null)
            {
                Title = "Add Study Program";
                ConfirmButton.Content = "Add";
            }
            else
            {
                Title = "Edit Study Program";
                ConfirmButton.Content = "Save";

                StudyProgram.StudyProgramId = program.StudyProgramId;
                StudyProgram.Name = program.Name;
                StudyProgram.Description = program.Description;
                StudyProgram.AvailableSeats = program.AvailableSeats;
            }

            this.DataContext = StudyProgram;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            StudyProgram.Identifier = $"{StudyProgram.Name.Substring(0, 3).ToUpper()}-{StudyProgram.AvailableSeats}";
            DialogResult = true;
            this.Close();
        }
    }
}
