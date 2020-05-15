using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gee_Whiz_Software_Test.ViewModels
{
    /// <summary>
    /// I made the submit type enum so that I could submit the BlockForecast form to a
    /// single action result and deal with everything once rather than having two functions.
    /// This just makes things a bit easier to read and follow in the code.
    /// </summary>
    public enum SubmitType
    {
        delete = 0,
        save = 1
    }

    public class BlockForecastViewModel
    {
        public string blockId { get; set; }
        [Range(2019, 2021)]
        public int cropYear { get; set; }
        public int forcastValue { get; set; }

        //This is going to track how the form was submitted.  Either a 1 for save or a 0 for delete.  Something like that.
        public SubmitType submitType { get; set; }
    }
}