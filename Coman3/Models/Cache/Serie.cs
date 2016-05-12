using System.Collections.Generic;

namespace WatchSeries
{
    public class Serie
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public List<Season> Seasons { get; set; }
    }
}