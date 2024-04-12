
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Database.Models
{
    public class School : INotifyPropertyChanged
    {
        public int SchoolId { get; set; }
        private string name = string.Empty;
        public string Name { get { return name; } set { SetProperty(ref name, value); } }
        private Address address = new();
        public Address Address { get { return address; } set { SetProperty(ref address, value); } }

        private ObservableCollection<StudyProgram> studyPrograms = new();
        public ObservableCollection<StudyProgram> StudyPrograms { get { return studyPrograms; } set { SetProperty(ref studyPrograms, value); } }
        public string StudyProgramsString => string.Join(", ", StudyPrograms.Select(x => x.Identifier));

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
            return $"{Name}";
        }
    }
}
