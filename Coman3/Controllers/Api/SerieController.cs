using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Coman3.Helpers;
using Coman3.Models;
using Coman3.Models.Api;
using Coman3.Models.Database;
using PagedList;

namespace Coman3.Controllers.Api
{
    public class SerieController : ApiController
    {
        private readonly ApplicationDbContext _dbContext = ApplicationDbContext.Create();
        private readonly SerieHelper _serieHelper;

        public SerieController()
        {
            _serieHelper = new SerieHelper(_dbContext);
        }
        /// <summary>
        /// Get a list of (Max) 20 items that relate to the "search" (id) query. 
        /// </summary>
        /// <param name="id">The Query that you wish to search, this query can only contain the name of the serie, from the start.</param>
        /// <returns>A List of "Bag.ItemsPerPage" <see cref="Serie"/>'s, with all data acociated.</returns>
        public List<Serie> Get(string id)
        {
            if (id == null || id.Length < 3) throw new InvalidOperationException("Id must be longer than 3 chars");
            return _serieHelper.GetSeriesFromQuery(new SerieIndexBag { ItemsPerPage = 20, Accending = true, Filter = id, OrderBy = SortOption.Name}).ToPagedList(1, 20).ToList();
        }

        /// <summary>
        /// Get A Paged List of <see cref="Serie"/>'s with all infomation about the serie, seasons and episodes
        /// </summary>
        /// <param name="bag">The Colection Modifyer Bag, can be collected from /Series/Index/ </param>
        /// <returns>A List of "Bag.ItemsPerPage" <see cref="Serie"/>'s, with all data acociated.</returns>
        public List<Serie> Get([FromUri] SerieIndexBag bag)
        {
            return _serieHelper.GetSeriesFromQuery(bag).ToPagedList(bag.Page, bag.ItemsPerPage).ToList();
        }

        public void Post()
        {
        }
    }
}
