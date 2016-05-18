using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Coman3.Controllers.Api
{
    public class GalleryController : ApiController
    {
        public List<Size> Sizes = new List<Size>
        {
            new Size(400, 400),
            new Size(450, 630),
            new Size(150, 200),
            new Size(150, 80),
            new Size(300, 190),
            new Size(140, 90)
        };
        //Returns a list of images related to the serie (id)
        public List<string> Get(Guid id, int page)
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            var imgs = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                var size = Sizes[rand.Next(Sizes.Count - 1)];
                imgs.Add($"http://placehold.it/{size.Width}x{size.Height}");
            }
            return imgs;
        } 
    }
}
