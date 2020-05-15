using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Gee_Whiz_Software_Test.Models
{
    /// <summary>
    /// A database repository is probably not needed for this project, but it's another thing I kind of got used to doing.
    /// The basic idea is that this works directly with the database so that other code doesn't have to.  Specifically,
    /// this does very little because all of the work is being done in stored procedures.  So this is literally just
    /// passing data into the stored procs.  Generally, this abstraction is worth having to look around a bit to see
    /// where things happen because it keeps the rest of the code using this stuff so clean.
    /// </summary>
    public class SoftwareTestRepository
    {
        public SoftwareTestDataContext Context;

        public SoftwareTestRepository()
        {
            Context = new SoftwareTestDataContext(ConfigurationManager.ConnectionStrings["SoftwareTestConnectionString"].ConnectionString);
        }

        public SoftwareTestRepository(SoftwareTestDataContext context)
        {
            Context = context;
        }

        //SoftwareTestDataContext Context = new SoftwareTestDataContext(ConfigurationManager.ConnectionStrings["SoftwareTestConnectionString"].ConnectionString)
        #region "Views"

        /// <summary>
        /// Get the data from the variety view.
        /// </summary>
        /// <returns></returns>
        public IQueryable<vw_Variety> GetVarieties()
        {
            return Context.vw_Varieties;
        }

        /// <summary>
        /// Get the data from the region view.
        /// </summary>
        /// <returns></returns>
        public IQueryable<vw_Region> GetRegions()
        {
            return Context.vw_Regions;
        }

        #endregion

        #region "Functions"

        /// <summary>
        /// This function will take a region Id and variety Id, to get blocks matching the values.
        /// </summary>
        /// <param name="regionId">Id from the Region table.</param>
        /// <param name="varietyId">Id from the Variety table.</param>
        /// <returns></returns>
        public IQueryable<fn_BlockSearchResult> GetBlockSearchResult(int? regionId, int? varietyId)
        {
            return Context.fn_BlockSearch(regionId, varietyId);
        }

        /// <summary>
        /// This function will take a block Id and get a harvest forecast.
        /// </summary>
        /// <param name="blockId">Id from the block table.</param>
        /// <returns></returns>
        public IQueryable<fn_GetHarvestForecastResult> GetHarvestForecastResult(string blockId)
        {
            return Context.fn_GetHarvestForecast(blockId);
        }

        /// <summary>
        /// Get the previous actual harvest results for a given block id.
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public IQueryable<fn_GetPreviousHarvestActualsResult> GetPreviousHarvestActualResult(string blockId)
        {
            return Context.fn_GetPreviousHarvestActuals(blockId);
        }

        /// <summary>
        /// Get the previous forecasts for a given block id.
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public IQueryable<fn_GetPreviousHarvestForecastResult> GetPreviousHarvestForecastResult(string blockId)
        {
            return Context.fn_GetPreviousHarvestForecast(blockId);
        }

        #endregion

        #region "Stored Procedure"

        /// <summary>
        /// This will modify a harvest forecast.  If the harvest forecast doesn't exist, create it.
        /// </summary>
        /// <param name="blockId">Block Id</param>
        /// <param name="cropYear">Year</param>
        /// <param name="forcastValue">Value of the forecast</param>
        /// <param name="retVal">Return value: 1-Record found and updated;2-Record did not exists, so created;3-Error, action rolled back;0-Nothing happened.Shouldn't be possible.</param>
        public void ModifyHarvestForecast(string blockId, int cropYear, int forcastValue, ref int? retVal)
        {
            Context.sp_ModifyHarvestForecast(blockId, cropYear, forcastValue, ref retVal);
        }

        /// <summary>
        /// This will delete a harvest forecast.
        /// </summary>
        /// <param name="blockId">Block Id</param>
        /// <param name="retVal">Return value: 1-Record found and updated.;2-Error, action rolled back.</param>
        public void DeleteHarvestForecast(string blockId, ref int? retVal)
        {
            Context.sp_DeleteHarvestForecast(blockId, ref retVal);
        }

        #endregion

        #region "Tables"


        #endregion
    }
}