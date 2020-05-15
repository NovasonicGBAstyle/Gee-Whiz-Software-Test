using Gee_Whiz_Software_Test.Models;
using Gee_Whiz_Software_Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gee_Whiz_Software_Test.Controllers
{
    public class BlockForecastController : Controller
    {
        public SoftwareTestRepository Repository;

        public BlockForecastController()
        {
            Repository = new SoftwareTestRepository();
        }

        public BlockForecastController(SoftwareTestRepository repo)
        {
            Repository = repo;
        }


        #region "Views"
        // GET: BlockSearch
        public ActionResult Index()
        {
            return RedirectToAction("BlockSearch","BlockSearch");
        }

        /// <summary>
        /// A search form allowing users to see a list of all blocks.Clicking on a block will open a Block Forecast form.
        /// The user should be able to filter the block list by variety and region. 
        /// </summary>
        /// <returns></returns>
        public ActionResult BlockForecast(string blockId)
        {
            //Make sure we have a block Id.
            if(blockId == null)
            {
                return RedirectToAction("BlockSearch", "BlockSearch");
            }
            //Start a view model.
            BlockForecastViewModel model = Repository.GetHarvestForecastResult(blockId).ToViewModel(blockId);

            return View(model);
        }

        /// <summary>
        /// This will actually handle the post with the data model and hopefully save or delete the forecast.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BlockForecast(BlockForecastViewModel model)
        {
            //This will hold the value returned from the stored proc to determine success of failure of the stored proc.
            int? retVal = 0;

            //First, we'll check to see if we're deleting this.  No real checks needed if that's the case.
            if (model.submitType == SubmitType.delete)
            {
                //Run the delete stored proc.
                Repository.DeleteHarvestForecast(model.blockId, ref retVal);

                if (retVal == 1)
                {
                    //Success.  Send user to success page.
                    return RedirectToAction("ForecastDeleteSuccess", "BlockForecast");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There was an error trying to delete the forecast.  Error code: " + retVal);
                    return View(model);
                }
            }
            else
            {

                //Do a couple quick checks here for data quality.
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //Check to see if this is a save or a delete.
                if (model.submitType == SubmitType.save)
                {
                    //This is a save.  Run the stored proc.
                    Repository.ModifyHarvestForecast(model.blockId, model.cropYear, model.forcastValue, ref retVal);

                    //Check the return value for successful update.
                    if (retVal == 1)
                    {
                        //Success.  Send to the update success page.
                        return RedirectToAction("ForecastUpdateSuccess", "BlockForecast");
                    }
                    //Check the return value for successful add.
                    else if(retVal == 2)
                    {
                        //Success.  Send to the add success page.
                        return RedirectToAction("ForecastAddedSuccess", "BlockForecast");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "There was an error trying to update the forecast.  Error code: " + retVal);
                        return View(model);
                    }
                }

                return View(model);
            }
        }

        /// <summary>
        /// Just a view showing success.
        /// </summary>
        /// <returns></returns>
        public ActionResult ForecastUpdateSuccess()
        {
            return View();
        }

        /// <summary>
        /// Just a view showing success.
        /// </summary>
        /// <returns></returns>
        public ActionResult ForecastDeleteSuccess()
        {
            return View();
        }

        /// <summary>
        /// Just a view showing that a forecast was successfully added.
        /// </summary>
        /// <returns></returns>
        public ActionResult ForecastAddedSuccess()
        {
            return View();
        }

        #endregion

        #region "Partial Views"
        /// <summary>
        /// This partial class shows up in the Block Forecast view.  It displays actual harvest results over time.
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public ActionResult _PreviousBlockActuals(string blockId)
        {
            PreviousBlockActualViewModel model = Repository.GetPreviousHarvestActualResult(blockId).ToViewModel();
            return PartialView(model);
        }

        /// <summary>
        /// This partial class shows up in the block forecast view.  It displays previous forecasts over time.
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public ActionResult _PreviousBlockForecasts(string blockId)
        {
            PreviousBlockForecastViewModel model = Repository.GetPreviousHarvestForecastResult(blockId).ToViewModel();
            return PartialView(model);
        }
        #endregion
    }
}