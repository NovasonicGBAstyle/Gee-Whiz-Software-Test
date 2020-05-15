using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gee_Whiz_Software_Test.ViewModels
{
    public class PreviousBlockForecastViewModel
    {
        public List<PreviousBlockForecast> previousBlockForecasts;
    }
    public class PreviousBlockForecast
    {
        public int cropYear { get; set; }
        public string blockId { get; set; }
        public int forecastValue { get; set; }
    }
}