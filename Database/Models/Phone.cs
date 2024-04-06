using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Phone : INotifyPropertyChanged
    {
        public Phone() { }

        public Phone(string code, string number) 
        { 
            Code = code; 
            Number = number;
        }
        public Phone(Phone other)
        {
            Code = other.Code;
            Number = other.Number;
        }
        public int PhoneId { get; set; }

        private string code = string.Empty;
        public string Code
        {
            get { return code; }
            set { SetProperty(ref code, value); }
        }

        private string number = string.Empty;
        public string Number
        {
            get { return number; }
            set { SetProperty(ref number, value); }
        }

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
            return $"{Code} {Number}";
        }
    }
}
