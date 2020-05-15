using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gee_Whiz_Software_Test.ViewModels
{
    public class BlockSearchResultViewModel
    {
        public List<BlockSearchResult> blockSearchResults;
    }

    public class BlockSearchResult
    {
        public string blockId { get; set; }
        public int regionId { get; set; }
        public string region { get; set; }
        public int varietyId { get; set; }
        public string varietyName { get; set; }
    }
}