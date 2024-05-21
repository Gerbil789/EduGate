using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models
{
    public class StudyProgram : INotifyPropertyChanged
    {
        public int StudyProgramId { get; set; }

        private string name = string.Empty;
        public string Name { get { return name; } set { SetProperty(ref name, value); } }


        private string identifier = string.Empty;
        public string Identifier { get { return identifier; } set { SetProperty(ref identifier, value); } }



        private string description = string.Empty;
        public string Description { get { return description; } set { SetProperty(ref description, value); } }



        private int availableSeats;
        public int AvailableSeats { get { return availableSeats; } set { SetProperty(ref availableSeats, value); } }



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
