using System;
using PagedList;

namespace Coman3.Models
{
    public class SerieIndexViewModel
    {

        public SerieIndexViewModel(IPagedList<Serie> pagedList, SerieIndexBag bag)
        {
            Series = pagedList;
            Bag = bag;
        }

        public SerieIndexBag Bag { get; set; }
        public IPagedList<Serie> Series { get; set; }
    }
    public class SerieIndexBag
    {
        public int Page { get; set; }
        public SortOption OrderBy { get; set; }
        public string Filter { get; set; }
        public bool Accending { get; set; }
        public bool CompactMode { get; set; }
        public bool EditMode { get; set; }
        public int Columns { get; set; }
        public bool ShowShortDesc { get; set; }
        public int ItemsPerPage { get; set; }

        public SerieIndexBag() { }

        public SerieIndexBag(SerieIndexBag bag)
        {
            Page = bag.Page;
            OrderBy = bag.OrderBy;
            Filter = bag.Filter;
            Accending = bag.Accending;
            CompactMode = bag.CompactMode;
            EditMode = bag.EditMode;
            Columns = bag.Columns;
            ShowShortDesc = bag.ShowShortDesc;
            ItemsPerPage = bag.ItemsPerPage;
        }

        public void Validate()
        {
            if (Page <= 0) Page = 1;
            if (Filter == null) Filter = "";
            if (Columns <= 0 || Columns > 12) Columns = 1;
            if (ItemsPerPage < 10 || ItemsPerPage > 1000) ItemsPerPage = 100;
        }

        public SerieIndexBag ChangeValue(Action<SerieIndexBag> modifierAction)
        {
            var newBag = new SerieIndexBag(this);
            modifierAction.Invoke(newBag);
            newBag.Validate();
            return newBag;
        }
    }

    public enum SortOption
    {
        Name,
        SeasonCount,
        EpisodeCount,
        DatePublished,
        LastEpisodeDate,
        DateAdded,
        MostViews,
        HighestRating,
    }
}