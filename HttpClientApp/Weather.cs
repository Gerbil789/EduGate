using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientApp
{
  public class Weather
  {
    public string product { get; set; }
    public string init { get; set; }
    public List<DataSeries> dataseries { get; set; }
  }

  public class DataSeries
  {
    public int timepoint { get; set; }
    public int temp2m { get; set; }
  }
}
