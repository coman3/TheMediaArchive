using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Coman3.Models;
using Coman3.Models.Database;

namespace Coman3.Controllers.Api
{
    public class TagController : ApiController
    {
        public ApplicationDbContext DbContext = ApplicationDbContext.Create();
        public List<Tag> Get(string id = null)
        {
            if (id == null) return DbContext.Tags.ToList();
            var lowId = id.ToLower();
            return DbContext.Tags.Where(x => x.Name.ToLower().StartsWith(lowId))
                .ToList();
        }
    }
}
