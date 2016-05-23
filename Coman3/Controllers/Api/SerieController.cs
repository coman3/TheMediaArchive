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
        
        public List<Serie> Get([FromUri] SerieIndexBag bag)
        {
            return _serieHelper.GetSeriesFromQuery(bag).ToPagedList(bag.Page, bag.ItemsPerPage).ToList();
        }

        public void Post()
        {
        }
    }
}
