using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Database.Models
{
    public class Application : INotifyPropertyChanged
    {
        public int ApplicationId { get; set; }

        private DateTime? submissionDate = null;
        public DateTime? SubmissionDate { get { return submissionDate; } set { SetProperty(ref submissionDate, value); } }

        private StudyProgram? studyProgram = null;
        public StudyProgram? StudyProgram { get { return studyProgram; } set { SetProperty(ref studyProgram, value); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public override string ToString()
        {
            return $"{StudyProgram}";
        }
    }
}
