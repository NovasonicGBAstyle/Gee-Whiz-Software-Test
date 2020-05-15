using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gee_Whiz_Software_Test.Models;
using Gee_Whiz_Software_Test.ViewModels;
using Microsoft.Ajax.Utilities;

namespace Gee_Whiz_Software_Test.Controllers
{
    /// <summary>
    /// This projections class is not really necessary for this project, but I find it to be very useful in large projects.  It helps
    /// keep code clean by handling all the parts that bring the data from the database into code.  If you have view models that have
    /// lists that are populated, this comes in really handy because those lists get emptied out every time the model gets posted.
    /// This is a nice clean way to have it done so that in the controller you call the function that lives here instead of doing
    /// all of that work in the controller.
    /// </summary>
    public static class Projections
    {
        #region "Simple Select Lists"
        /// <summary>
        /// This will take the get varieties from the repo and turn it into a select list.
        /// </summary>
        /// <param name="repo"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetVarieties_SelectList(this SoftwareTestRepository repo)
        {
            return repo.GetVarieties()
                .Select(x => new SelectListItem()
                {
                    Value = x.VarietyID.ToString(),
                    Text = x.VarietyName
                }).OrderBy(x => x.Text).ToList();
        }

        /// <summary>
        /// This will take the get regions from the repo and turn it into a select list.
        /// </summary>
        /// <param name="repo"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetRegions_SelectList(this SoftwareTestRepository repo)
        {
            return repo.GetRegions()
                .Select(x => new SelectListItem()
                {
                    Value = x.RegionId.ToString(),
                    Text = x.RegionName
                }).OrderBy(x => x.Text).ToList();
        }
        #endregion

        #region "View Model Population"
        /// <summary>
        /// This will take a BlockSearchViewModel like the one used in BlockSearch and fill it's select lists.
        /// </summary>
        /// <param name="model">Self</param>
        /// <param name="repo">Repository that has the data.</param>
        /// <returns></returns>
        public static BlockSearchViewModel PopualteDomainValues(this BlockSearchViewModel model, SoftwareTestRepository repo)
        {
            model.regions = GetRegions_SelectList(repo);
            model.varieties = GetVarieties_SelectList(repo);
            return model;
        }

        /// <summary>
        /// This is used when brining up a forecast for modification/creation/deletion I suppose.
        /// </summary>
        /// <param name="results"></param>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public static BlockForecastViewModel ToViewModel(this IQueryable<fn_GetHarvestForecastResult> results, string blockId)
        {
            //See if there were results.
            if(results == null || results.Count()==0)
            {
                //Nothing popped up, so we'll create a new view model to return.
                return new BlockForecastViewModel
                {
                    //Set default values.
                    blockId = blockId,
                    cropYear = DateTime.Now.Year
                };
            }
            else
            {
                //Build a new view model.
                return results
                    .Select(x => new BlockForecastViewModel
                    {
                        blockId = x.BlockID,
                        cropYear = x.CropYear,
                        forcastValue = x.ForcastValue
                    }).FirstOrDefault();
            }
        }

        /// <summary>
        /// This is used when we do a block search.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static BlockSearchResultViewModel ToViewModel(this IQueryable<fn_BlockSearchResult> results)
        {
            //See if we got nothing back.
            if (results == null)
            {
                //Return an empty view model.
                return new BlockSearchResultViewModel();
            }
            else
            {
                //Build a view model.
                BlockSearchResultViewModel model = new BlockSearchResultViewModel();

                //Select the values we need fom the function results into our block search results list.
                model.blockSearchResults = results.Select(x => new BlockSearchResult
                {
                    blockId = x.BlockID,
                    regionId = x.RegionId,
                    region = x.Region,
                    varietyId = x.VarietyID,
                    varietyName = x.VarietyName
                }).ToList();

                return model;
            }
        }

        /// <summary>
        /// This is used by the block forecast partial view.  It shows the actuals for this block over time.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static PreviousBlockActualViewModel ToViewModel(this IQueryable<fn_GetPreviousHarvestActualsResult> results)
        {
            if(results == null)
            {
                return new PreviousBlockActualViewModel();
            }
            else
            {
                //Build new model.
                PreviousBlockActualViewModel model = new PreviousBlockActualViewModel();
                model.previousBlockActuals = results
                    .OrderBy(x=>x.HarvestDate)
                    .Select(x => new PreviousBlockActual
                {
                    cropYear = x.CropYear,
                    harvestDate = x.HarvestDate,
                    blockId = x.BlockID,
                    bins = x.Bins
                }).ToList();

                return model;
            }
        }
        
        /// <summary>
        /// This is used by the block forecast partial view.It shows the forecasts for this block over time.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static PreviousBlockForecastViewModel ToViewModel(this IQueryable<fn_GetPreviousHarvestForecastResult> results)
        {

            if (results == null)
            {
                return new PreviousBlockForecastViewModel();
            }
            else
            {
                //Build new model.
                PreviousBlockForecastViewModel model = new PreviousBlockForecastViewModel();
                model.previousBlockForecasts = results
                    .OrderBy(x => x.CropYear)
                    .Select(x => new PreviousBlockForecast
                {
                    cropYear = x.CropYear,
                    blockId = x.BlockID,
                    forecastValue = x.ForcastValue
                }).ToList();

                return model;
            }
        }
        #endregion
    }
}