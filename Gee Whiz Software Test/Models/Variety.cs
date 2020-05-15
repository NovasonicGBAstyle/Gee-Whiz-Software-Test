using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gee_Whiz_Software_Test.Models
{
    /// <summary>
    /// This class is based on the variety table in the database.
    /// </summary>
    public class Variety
    {
        public long Varietyid { get; set; }
        public string VarietyName { get; set; }
    }
}