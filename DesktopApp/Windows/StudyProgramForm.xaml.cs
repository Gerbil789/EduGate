using Database.Models;
using DesktopApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                StudyProgram.Identifier = program.Identifier;
            }

            this.DataContext = StudyProgram;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            StudyProgram.Identifier = StudyProgramHelper.GenerateProgramIdentifier(StudyProgram);
            DialogResult = true;
            this.Close();
        }
    }
}
