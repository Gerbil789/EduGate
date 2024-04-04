using Database.Models;
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

namespace DesktopApp
{
  /// <summary>
  /// Interaction logic for StudentForm.xaml
  /// </summary>
  public partial class StudentForm : Window
  {
    public StudentForm(Student student)
    {
      InitializeComponent();

      this.DataContext = student;
    }

    private void SaveStudent(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
  }
}
