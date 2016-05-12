using System.Collections.Generic;

namespace WatchSeries
{
    public class Season
    {
        public int Number { get; set; }
        public string SeasonText { get; set; }
        public string Name => "Season " + Number;
        public List<Episode> Episodes { get; set; }
    }
}