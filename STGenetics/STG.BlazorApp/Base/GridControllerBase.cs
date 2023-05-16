using STG.BlazorApp.Helpers;

namespace STG.BlazorApp.Base
{
    public abstract class GridControllerBase<T>
    {
        protected List<T> items;
        protected PaginationHelper<T> paginationHelper;
        protected SortingHelper<T> sortingHelper;
        protected FilteringHelper<T> filteringHelper;

        protected int pageSize = 10;
        protected string sortColumn;
        protected bool sortAscending;

        protected GridControllerBase(List<T> items)
        {
            this.items = items;
            paginationHelper = new PaginationHelper<T>();
            paginationHelper.PageSize = pageSize;
            paginationHelper.SetItems(items);

            sortingHelper = new SortingHelper<T>(items);
            filteringHelper = new FilteringHelper<T>(items);
        }

        public bool IsDataLoaded { get; set; } = false;

        public List<T> Items => paginationHelper.GetPageItems();

        public int CurrentPage
        {
            get => paginationHelper.CurrentPage;
            set
            {
                paginationHelper.CurrentPage = value;
                UpdateDisplayedItems();
            }
        }

        public int TotalPages => paginationHelper.TotalPages;
        public bool IsFirstPage => CurrentPage == 1;
        public bool IsLastPage => CurrentPage == TotalPages;
        public bool IsPreviousPage => CurrentPage > 1;
        public bool IsNextPage => CurrentPage < TotalPages;

        protected void UpdateDisplayedItems()
        {
            paginationHelper.SetItems(items);
        }

        public void GoToFirstPage()
        {
            if (CurrentPage != 1)
            {
                CurrentPage = 1;
                UpdateDisplayedItems();
            }
        }

        public void GoToPreviousPage()
        {
            if (IsPreviousPage)
            {
                CurrentPage--;
                UpdateDisplayedItems();
            }
        }

        public void GoToNextPage()
        {
            if (IsNextPage)
            {
                CurrentPage++;
                UpdateDisplayedItems();
            }
        }

        public void GoToLastPage()
        {
            if (CurrentPage != TotalPages)
            {
                CurrentPage = TotalPages;
                UpdateDisplayedItems();
            }
        }
    }
}
