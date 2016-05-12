using System.Collections.Generic;

namespace WatchSeries
{
    public class Episode
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public List<string> VideoLinks { get; set; }
    }
}