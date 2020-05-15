using Gee_Whiz_Software_Test.Models;
using Gee_Whiz_Software_Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gee_Whiz_Software_Test.Controllers
{
    public class BlockSearchController : Controller
    {
        public SoftwareTestRepository Repository;

        public BlockSearchController()
        {
            Repository = new SoftwareTestRepository();
        }

        public BlockSearchController(SoftwareTestRepository repo)
        {
            Repository = repo;
        }

        #region "Views"
        // GET: BlockSearch
        public ActionResult Index()
        {
            return RedirectToAction("BlockSearch");
        }

        /// <summary>
        /// A search form allowing users to see a list of all blocks.Clicking on a block will open a Block Forecast form.
        /// The user should be able to filter the block list by variety and region.
        /// Side note: this is the main page.  All the index pages are redirecting to this.
        /// </summary>
        /// <returns></returns>
        public ActionResult BlockSearch()
        {
            //Start a view model.
            var model = new BlockSearchViewModel();

            //Populate domain values and such.
            model.PopualteDomainValues(Repository);

            return View(model);
        }

        #endregion

        #region "Partial Views"
        /// <summary>
        /// This is the partial view that is called when the search button is hit on the block search page.
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="varietyId"></param>
        /// <returns></returns>
        public ActionResult _BlockSearchResult(int regionId, int varietyId)
        {
            BlockSearchResultViewModel model = Repository.GetBlockSearchResult(regionId, varietyId).ToViewModel();
            //BlockSearchResultViewModel model = Repository.GetBlockSearchResult(null, null).ToViewModel();
            return PartialView(model);
        }
        
        #endregion
    }
}