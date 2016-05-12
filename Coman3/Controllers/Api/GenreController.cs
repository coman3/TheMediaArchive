using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Coman3.Models;
using Coman3.Models.Api;
using Coman3.Models.Database;

namespace Coman3.Controllers.Api
{
    public class GenreController : ApiController
    {
        public ApplicationDbContext DbContext = ApplicationDbContext.Create();
        public List<Genre> Get(string id = null)
        {
            if (id == null) return DbContext.Genres.ToList();
            var lowId = id.ToLower();
            return DbContext.Genres.Where(x => x.Name.ToLower().StartsWith(lowId))
                .ToList();
        }
    }
}
