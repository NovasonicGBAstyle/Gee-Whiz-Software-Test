using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gee_Whiz_Software_Test.Models
{
    /// <summary>
    /// This class is based on the region table in the database.
    /// </summary>
    public class Region
    {
        public long RegionId { get; set; }
        public string RegionName { get; set; }
    }
}