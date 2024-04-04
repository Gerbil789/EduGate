using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Database.Models
{
  public class Student : INotifyPropertyChanged
  {
    public int StudentId { get; set; }

    private string firstName;
    public string FirstName
    {
      get { return firstName; }
      set { Notify(ref firstName, value); }
    }

    private string lastName;
    public string LastName {
      get { return lastName; }
      set { Notify(ref lastName, value); } 
    }
    public Address Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; } // -> age
    public string BirthPlace { get; set; }


    private void Notify<T>(ref T prop, T val,[CallerMemberName] string name = null)
    {
      prop = val;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
  }
}
