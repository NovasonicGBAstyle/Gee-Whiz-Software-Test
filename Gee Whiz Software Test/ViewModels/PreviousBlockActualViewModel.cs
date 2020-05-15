using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gee_Whiz_Software_Test.ViewModels
{
    public class PreviousBlockActualViewModel
    {
        public List<PreviousBlockActual> previousBlockActuals;
    }
    public class PreviousBlockActual
    {
        public int cropYear { get; set; }
        public string blockId { get; set; }
        public DateTime harvestDate { get; set; }

        public int bins { get; set; }
    }
}