using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gee_Whiz_Software_Test.Models;

namespace Gee_Whiz_Software_Test.ViewModels
{
    /// <summary>
    /// View model to be used by the block search class.
    /// </summary>
    public class BlockSearchViewModel
    {
        public int searchVarietyId { get; set; }
        public int searchRegionId { get; set; }
        //This will hold our varieties.
        public List<SelectListItem> varieties;

        //This will hold our regions
        public List<SelectListItem> regions;

    }
}